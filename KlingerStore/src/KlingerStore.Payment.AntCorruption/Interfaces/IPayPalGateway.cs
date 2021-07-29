namespace KlingerStore.Payment.AntCorruption.Interfaces
{
    public interface IPayPalGateway
    {
        string GetPayPalServiceKey(string apiKey, string encriptionKey);
        string GetCardHashKey(string serviceKey, string creditCart);
        bool CommitTransaction(string cartHashKey, string orderId, decimal amount);
    }
}
