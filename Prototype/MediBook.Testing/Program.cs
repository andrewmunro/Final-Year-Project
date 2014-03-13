using System;

using MediBook.Client.Core;
using MediBook.Client.Core.Components.Account;

namespace MediBook.Testing
{
    class Program
    {
        public static AppCore AppCore { get; private set; }

        static void Main(string[] args)
        {
            AppCore = new AppCore();

            new System.Threading.Timer(obj => AppCore.GetComponent<AccountComponent>().Register("andrew", "password"), null, 10000, System.Threading.Timeout.Infinite);

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
