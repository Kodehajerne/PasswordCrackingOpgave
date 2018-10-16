using System.Threading;
using System.Threading.Tasks;

namespace PasswordCrackerCentralized
{
    class Program
    {
        static void Main()
        {
            Cracking cracker = new Cracking();
            //cracker.RunCracking();

            Thread myThread = new Thread(new ThreadStart(cracker.RunCracking()));
            Thread myThread2 = new Thread(new ThreadStart(cracker.RunCracking()));

           

            //Task.Factory.StartNew(() => cracker.RunCracking());
        }
    }
}
