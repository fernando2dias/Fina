using Fina.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Common.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Título invalido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo invalido")]
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

        [Required(ErrorMessage = "Valor invalido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage ="Categoria invalida")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data invalida")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
