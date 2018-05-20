using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobCoin.MIXR.CL;

namespace JobCoin.MIXR
{
    class Program
    {       

        static void Main(string[] args)
        {
            string houseAccount = "THouse";
            string commissionAccount = "TComm";
            string test = "http://localhost:26684/";
            string production = "http://ugoforapi.azurewebsites.net/";
            Poller p = new Poller(production);
            p.Start(commissionAccount, houseAccount);
            Console.WriteLine("Press Enter To Exit");
            Console.ReadLine();
        }
 

    }
}
