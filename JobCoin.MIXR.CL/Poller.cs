using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobCoin.MIXR.CL
{
    public class Poller
    {
        private CancellationTokenSource serverCancel;
        private static readonly Random random = new Random();
        private string restURLEndpoint;
        public Poller(string RESTURLEndpoint)
        {
            this.restURLEndpoint = RESTURLEndpoint;
            this.serverCancel = new CancellationTokenSource();
        }

        public async void Start(string commissionAccountName, string houseAccountName)
        {
            try
            {
                Console.WriteLine("Begin Polling Network");
                while (!this.serverCancel.IsCancellationRequested)
                {
                    await Task.Run(async() =>
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("Scanning for Queued Forward Addresses");
                        List<Address> addresses = await this.GetAddressesAsync(1);
                        Console.WriteLine("Retrieved {0} Newly Queued Forward Addresses", addresses.Count());
                        Thread.Sleep(500);
                        Console.WriteLine("Scanning for Transactions on the JobCoin Network");
                        List<Transaction> transactions = await this.GetTransactionsAsync();
                        var nameInOne = transactions.Where(t2 => addresses.Any(t1 => t2.toAddress.Contains(t1.DepositAddress)));
                        Console.WriteLine("Retrieved {0} Transactions with funds sent to Generated Deposit Addresses", nameInOne.Count());
                        foreach (var n in nameInOne)
                        {
                            string newAmount = await SendToHouseAsync(n.amount, n.fromAddress, commissionAccountName, houseAccountName, n.toAddress);
                            SimpleSend submitaddr = new SimpleSend { Amount = newAmount, DepositAddress = n.toAddress, Status=2};
                            await PutAddressesAsync(submitaddr);
                        }

                        await DoleFromHouseAsync(houseAccountName);

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Address>> GetAddressesAsync(int status)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(restURLEndpoint);
                    var response = await client.GetAsync("/api/depositaddress?status="+status);
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawAddress = JsonConvert.DeserializeObject<List<Address>>(stringResult);
                    return rawAddress;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }

        public async Task PutAddressesAsync(SimpleSend submitaddr)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(restURLEndpoint);
                    var stringContent = new StringContent(JsonConvert.SerializeObject(submitaddr), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync("/api/depositaddress", stringContent);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public async Task PostHouseAmountAsync(SimpleTrans simpleTrans)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://jobcoin.gemini.com");
                    var stringContent = new StringContent(JsonConvert.SerializeObject(simpleTrans), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("/headstone/api/transactions", stringContent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://jobcoin.gemini.com");
                    var response = await client.GetAsync("/headstone/api/transactions");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawAddress = JsonConvert.DeserializeObject<List<Transaction>>(stringResult);
                    return rawAddress;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }

        public async Task<string> SendToHouseAsync(string amountT, string fromAddr, string commisionAccountAddr, string houseAccountAddr, string depositAddress)
        {
            decimal amount = Decimal.Parse(amountT);
            decimal commission = amount * .03M;
            decimal leftOver = amount - commission;
            
            //send commission to commision account
            await PostHouseAmountAsync(new SimpleTrans { amount = commission.ToString(), fromAddress = depositAddress, toAddress = commisionAccountAddr });
            
            //send leftOver to house
            await PostHouseAmountAsync(new SimpleTrans { amount = leftOver.ToString(), fromAddress = depositAddress, toAddress = houseAccountAddr });
            
            return leftOver.ToString();
        }

        public async Task DoleFromHouseAsync(string fromHouseAddress)
        {
            List<Address> a = await GetAddressesAsync(2);

            if (a.Count > 0)
            {
                Console.WriteLine("Begin Shuffle/Random Distribution of Funds to {0} Addresses", a.Count);
            }

            Address[] aa = a.ToArray();
            Utilities.Shuffle<Address>(random,aa);            
            //array shuffled
            foreach(Address a1 in aa)
            {
                await Task.Run(async () =>
                {
                    await DistFundsTimeAsync(a1, fromHouseAddress);
                });
            }
        }

        private async Task DistFundsTimeAsync(Address a1, string fromHouseAddress)
        {
            List<string> randomDividedAmounts = Utilities.GetDistributions(random,a1.AmountToSend);
            foreach (string da in randomDividedAmounts)
            {
                int sleep = random.Next(1, 10);
                Console.WriteLine("Waiting {0} seconds before distributing {1} jobcoins to {2}", sleep, da, a1.SubmittedAddr);
                Thread.Sleep(sleep * 1000);
                SimpleTrans final = new SimpleTrans { amount = da, fromAddress = fromHouseAddress, toAddress = a1.SubmittedAddr };
                await PostHouseAmountAsync(final);
            }

            SimpleSend submitaddr = new SimpleSend { Amount = "", DepositAddress = a1.DepositAddress, Status = 3 };
            await PutAddressesAsync(submitaddr);

            if (randomDividedAmounts.Count > 0)
            {
                Console.WriteLine("Distribution to Account {0} Completed", a1.SubmittedAddr);
            }
        }

    }


}
