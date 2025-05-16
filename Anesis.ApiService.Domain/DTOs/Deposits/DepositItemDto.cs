namespace Anesis.ApiService.Domain.DTOs.Deposits
{
    public class DepositItemDto
    {
        public int DepositBatchId { get; set; }

        public int ItemNumber { get; set; }

        public string DepositType { get; set; }

        public string ReceivedFrom { get; set; }

        public DateTime ReceivedDate { get; set; }

        public decimal ItemAmount { get; set; }

        public string ReceivedBy { get; set; }

        public string Status { get; set; }

        public bool IsAutoLinked { get; set; }
    }
}
