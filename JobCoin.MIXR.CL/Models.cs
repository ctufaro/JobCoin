using System;
using System.Collections.Generic;
using System.Text;

namespace JobCoin.MIXR.CL
{
    public class Address
    {
        public int? Id { get; set; }
        public string SubmittedAddr { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string DepositAddress { get; set; }
        public int? Status { get; set; }
        public DateTime? StatusUpdate { get; set; }
        public string AmountToSend { get; set; }

        public Address()
        {

        }
    }

    public class Transaction
    {
        public DateTime? timestamp;
        public string toAddress;
        public string fromAddress;
        public string amount;

        public Transaction()
        {

        }
    }

    public class SimpleSend
    {
        public string DepositAddress;
        public string Amount;
        public int Status;
    }

    public class SimpleTrans
    {
        public string fromAddress;
        public string toAddress;
        public string amount;
    }
}
