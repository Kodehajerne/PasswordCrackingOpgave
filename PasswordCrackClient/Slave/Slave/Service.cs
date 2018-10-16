using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using Slave.Model;
using Slave.Util;
using Slave.CrackingMethods;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Slave
{
    /// <summary>
    /// Service class controll all the thing related to comunication. 
    /// </summary>
    public class Service
    {
        private TcpClient connectionSocket;
        private IList<UserInfo> _receivedDataString;
        private int _confirmChunckSize;
        private List<string> arrayOfName = new List<string>();  //Contains alle usernames 
        private List<string> arrayOfPassword = new List<string>();
        private static readonly HashAlgorithm _messageDigest = new SHA1CryptoServiceProvider();

        public Service(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }

        public void EnvokeCracking()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            while (message != null && message != "")
            {
                if (message == "start" || message == "Start" || message == "s" || message == "S")
                {
                    //Console.WriteLine("Cracking is started");
                    //Cracking cracker = new Cracking();

                    //Task.Factory.StartNew(() => { cracker.RunCracking(); });


                    Stopwatch stopwatch = Stopwatch.StartNew();

                    List<UserInfo> userInfos =
                        PasswordFileHandler.ReadPasswordFile("passwords.txt");
                    Console.WriteLine("passwd opeend");

                    List<UserInfoClearText> result = new List<UserInfoClearText>();

                    using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))

                    using (StreamReader dictionary = new StreamReader(fs))
                    {
                        while (!dictionary.EndOfStream)
                        {
                            String dictionaryEntry = dictionary.ReadLine();
                            IEnumerable<UserInfoClearText> partialResult = CheckWordWithVariations(dictionaryEntry, userInfos);
                            result.AddRange(partialResult);
                        }
                        var sendResult = JsonConvert.SerializeObject(result);
                        sw.WriteLine(sendResult);

                    }
                }
                else { throw new ArgumentException(); }
            }
        }

    internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            while (message != null && message != "")
            {
                //Recieve data from Master
                Console.WriteLine("Receiving data");
                _receivedDataString = JsonConvert.DeserializeObject<List<UserInfo>>(message);
               
                foreach (var item in _receivedDataString)
                {
                    string[] arrayUSerAndPAss = item.ToString().Split(':');
                    string name = arrayUSerAndPAss[0];
                    string password = arrayUSerAndPAss[1];
                    arrayOfName.Add(name);
                    arrayOfPassword.Add(password);
                }

                //Writes a passwordfile and saves it in the debug folder
                //PasswordFileHandler.WritePasswordFile("passwordtestfile", arrayOfName.ToArray(), arrayOfPassword.ToArray());

                //Sends back an confirmation
                sw.WriteLine("ok");


                ////Confirmes chunck size
                _confirmChunckSize = Convert.ToInt32(sr.ReadLine());
                sw.WriteLine($"chucnk size is set to: {_confirmChunckSize}");

                EnvokeCracking();

                Console.ReadLine();
            }
            ns.Close();
            connectionSocket.Close();
        }





        /// <summary>
        /// Generates a lot of variations, encrypts each of the and compares it to all entries in the password file
        /// </summary>
        /// <param name="dictionaryEntry">A single word from the dictionary</param>
        /// <param name="userInfos">List of (username, encrypted password) pairs from the password file</param>
        /// <returns>A list of (username, readable password) pairs. The list might be empty</returns>
        static IEnumerable<UserInfoClearText> CheckWordWithVariations(String dictionaryEntry, List<UserInfo> userInfos)
        {
            List<UserInfoClearText> result = new List<UserInfoClearText>(); //might be empty

            String possiblePassword = dictionaryEntry;
            IEnumerable<UserInfoClearText> partialResult = CheckSingleWord(userInfos, possiblePassword);
            Console.WriteLine("Normale words");
            result.AddRange(partialResult);

            String possiblePasswordUpperCase = dictionaryEntry.ToUpper();
            IEnumerable<UserInfoClearText> partialResultUpperCase = CheckSingleWord(userInfos, possiblePasswordUpperCase);
            Console.WriteLine("Uppercase check");
            result.AddRange(partialResultUpperCase);

            String possiblePasswordCapitalized = StringUtilities.Capitalize(dictionaryEntry);
            IEnumerable<UserInfoClearText> partialResultCapitalized = CheckSingleWord(userInfos, possiblePasswordCapitalized);
            Console.WriteLine("Capitalized check");
            result.AddRange(partialResultCapitalized);

            String possiblePasswordReverse = StringUtilities.Reverse(dictionaryEntry);
            IEnumerable<UserInfoClearText> partialResultReverse = CheckSingleWord(userInfos, possiblePasswordReverse);
            Console.WriteLine("Reverse check");
            result.AddRange(partialResultReverse);

            for (int i = 0; i < 100; i++)
            {
                String possiblePasswordEndDigit = dictionaryEntry + i;
                IEnumerable<UserInfoClearText> partialResultEndDigit = CheckSingleWord(userInfos, possiblePasswordEndDigit);
                result.AddRange(partialResultEndDigit);
            }

            for (int i = 0; i < 100; i++)
            {
                String possiblePasswordStartDigit = i + dictionaryEntry;
                IEnumerable<UserInfoClearText> partialResultStartDigit = CheckSingleWord(userInfos, possiblePasswordStartDigit);
                result.AddRange(partialResultStartDigit);
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    String possiblePasswordStartEndDigit = i + dictionaryEntry + j;
                    IEnumerable<UserInfoClearText> partialResultStartEndDigit = CheckSingleWord(userInfos, possiblePasswordStartEndDigit);
                    result.AddRange(partialResultStartEndDigit);
                }
            }

            return result;
        }


        /// <summary>
        /// Checks a single word (or rather a variation of a word): Encrypts and compares to all entries in the password file
        /// </summary>
        /// <param name="userInfos"></param>
        /// <param name="possiblePassword">List of (username, encrypted password) pairs from the password file</param>
        /// <returns>A list of (username, readable password) pairs. The list might be empty</returns>
        static IEnumerable<UserInfoClearText> CheckSingleWord(IEnumerable<UserInfo> userInfos, String possiblePassword)
        {
            char[] charArray = possiblePassword.ToCharArray();
            byte[] passwordAsBytes = Array.ConvertAll(charArray, PasswordFileHandler.GetConverter());

            byte[] encryptedPassword = _messageDigest.ComputeHash(passwordAsBytes);
            //string encryptedPasswordBase64 = System.Convert.ToBase64String(encryptedPassword);

            List<UserInfoClearText> results = new List<UserInfoClearText>();

            foreach (UserInfo userInfo in userInfos)
            {
                if (CompareBytes(userInfo.EntryptedPassword, encryptedPassword))  //compares byte arrays
                {
                    results.Add(new UserInfoClearText(userInfo.Username, possiblePassword));
                    Console.WriteLine(userInfo.Username + " " + possiblePassword);
                }
            }
            return results;
        }


        private static bool CompareBytes(IList<byte> firstArray, IList<byte> secondArray)
        {
            //if (secondArray == null)
            //{
            //    throw new ArgumentNullException("firstArray");
            //}
            //if (secondArray == null)
            //{
            //    throw new ArgumentNullException("secondArray");
            //}
            if (firstArray.Count != secondArray.Count)
            {
                return false;
            }
            for (int i = 0; i < firstArray.Count; i++)
            {
                if (firstArray[i] != secondArray[i])
                    return false;
            }
            return true;
        }


    }
}

