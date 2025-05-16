using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.BankTransactions
{
    public class BankAdjustmentCreateDto
    {
        public DateTime? PostDate { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public BankTransaction ToBankTransaction()
        {
            return new BankTransaction()
            {
                PostDate = PostDate.Value,
                Reference = Reference,
                Description1 = "adjustment",
                Description2 = Description,
                TransactionType = TransactionType,
                Debit = null,
                Credit = Amount,
                HashKey = DateTime.Now.Ticks.ToString()
            };
        }
    }
}
