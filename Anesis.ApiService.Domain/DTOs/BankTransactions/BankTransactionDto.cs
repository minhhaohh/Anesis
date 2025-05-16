
using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.BankTransactions
{
    public class BankTransactionDto
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string Reference { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public string TransactionType { get; set; }

        public decimal Balance { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        public bool ReconciledFlag { get; set; }

        public int? ReconciliationId { get; set; }

        public string HashKey { get; set; }

        public bool HasDepositDetails { get; set; }

        public bool HasCreditDetails { get; set; }

        public bool CanSplit => IsDeposit() || IsMerchantDeposit() || IsCreditCard();

        public bool IsDeposit()
        {
            var desc1 = Description1?.ToUpper() ?? "#N/A";
            return BankKeywordSettings.BankDepositDescription1.Contains(desc1);
        }

        public bool IsMerchantDeposit()
        {
            var desc1 = Description1?.ToUpper() ?? "";
            var reference = Reference?.ToUpper() ?? "#N/A";

            return desc1 == BankKeywordSettings.AchCreditDescription &&
                   BankKeywordSettings.BankDepositReference.Contains(reference);
        }

        public bool IsCreditCard()
        {
            var desc1 = Description1?.ToUpper() ?? "";
            var reference = Reference?.ToUpper() ?? "";

            return desc1 == BankKeywordSettings.AchCreditDescription && BankKeywordSettings.BankCreditCardReference.Contains(reference);
        }
    }
}
