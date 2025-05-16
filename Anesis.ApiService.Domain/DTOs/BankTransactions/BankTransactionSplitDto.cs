namespace Anesis.ApiService.Domain.DTOs.BankTransactions
{
    public class BankTransactionSplitDto
    {
        public int Id { get; set; }

        public decimal SplitAmount { get; set; }

        public string Notes { get; set; }
    }
}
