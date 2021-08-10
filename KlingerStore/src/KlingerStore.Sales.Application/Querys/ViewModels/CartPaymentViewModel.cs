using System.ComponentModel.DataAnnotations;

namespace KlingerStore.Sales.Application.Querys.ViewModels
{
    public class CartPaymentViewModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CardName { get; set; }

        [CreditCard]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NumberCart { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string ExpirationCart { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(4, ErrorMessage = "Minímo de {1} caracteres e máximo de {2} caracteres", MinimumLength = 3)]
        public string CvvCart { get; set; }
    }
}
