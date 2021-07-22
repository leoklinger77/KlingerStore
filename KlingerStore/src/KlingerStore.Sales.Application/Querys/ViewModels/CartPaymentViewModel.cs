using System.ComponentModel.DataAnnotations;

namespace KlingerStore.Sales.Application.Querys.ViewModels
{
    public class CartPaymentViewModel
    {
        public string CardName { get; set; }

        [CreditCard]
        public string NumberCart { get; set; }
        public string ExpirationCart { get; set; }
        public string CvvCart { get; set; }
    }
}
