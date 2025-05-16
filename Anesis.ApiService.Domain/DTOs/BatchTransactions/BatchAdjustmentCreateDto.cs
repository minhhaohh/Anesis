using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.BatchTransactions
{
    public class BatchAdjustmentCreateDto
    {
        public int? LocationId { get; set; }

        public string Description { get; set; }

        public string PaymentType { get; set; }

        public decimal Amount { get; set; }

        public BatchTransaction ToBatchTransaction()
        {
            return new BatchTransaction()
            {
                BankTransactionId = 0,
                BatchId = null,
                BatchOwner = "haotm",
                Description = Description,
                LocationId = LocationId,
                PaymentAmount = Amount,
                PaymentFrom = "adjustment",
                PaymentType = PaymentType,
                PostDate = DateTime.Now.Date,
                ReconciledFlag = false,
                ReconciliationId = null,
                TransactionType = "adjustment",
                SourceSite = LocationId == 17 ? "SOUNDASC" : "ANESIS"
            };
        }
    }
}
