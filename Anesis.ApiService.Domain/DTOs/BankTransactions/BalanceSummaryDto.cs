namespace Anesis.ApiService.Domain.DTOs.BankTransactions
{
    public class BalanceSummaryDto
    {
        public decimal BankTotal { get; set; }

        public decimal MatchedTotal { get; set; }

        public decimal Different => BankTotal - MatchedTotal;
    }
}
