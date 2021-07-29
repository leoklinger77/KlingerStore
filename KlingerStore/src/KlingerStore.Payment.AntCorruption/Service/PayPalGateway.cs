using KlingerStore.Payment.AntCorruption.Interfaces;
using System;
using System.Linq;

namespace KlingerStore.Payment.AntCorruption.Service
{
    public class PayPalGateway : IPayPalGateway
    {
        public bool CommitTransaction(string cartHashKey, string orderId, decimal amount)
        {
            return new Random().Next(2) == 0;
        }

        public string GetCardHashKey(string serviceKey, string creditCart)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXZ0123456789", 10).Select(x => x[new Random().Next(x.Length)]).ToArray());
        }

        public string GetPayPalServiceKey(string apiKey, string encriptionKey)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXZ0123456789", 10).Select(x => x[new Random().Next(x.Length)]).ToArray());
        }
    }
}
