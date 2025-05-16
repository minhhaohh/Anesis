using Anesis.Web.Data.Models;
using System.Net.Http;

namespace Anesis.Web.Data.Services
{
    public interface IReconciliationService
    {
        // RECONCILIATIONS
        Task<ResponseModel<List<ReconciliationViewModel>>> GetAllReconciliationsAsync(
            CancellationToken cancellationToken = default);

        Task<ResponseModel<ReconciliationViewModel>> GetLastTimeReconciliationAsync(
            CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateReconciliationAsync(
          DateTime? reconciledDate, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UndoReconciliationAsync(
           int id, CancellationToken cancellationToken = default);

        // BANK TRANSACTIONS
        Task<ResponseModel<List<BankTransactionViewModel>>> SearchBankTransactionsAsync(
            BankTransactionFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<BankTransactionViewModel>> GetBankTransactionByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<BalanceSummaryViewModel>> GetBalanceSummary(
            BankTransactionFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateBankAdjustmentAsync(
           BankAdjustmentCreateModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> SplitBankTransactionAsync(
            BankTransactionSplitModel model, CancellationToken cancellationToken = default);

        // BATCH TRANSACTIONS
        Task<ResponseModel<List<BatchTransactionViewModel>>> SearchBatchTransactionsAsync(
            BatchTransactionFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<BatchTransactionViewModel>> GetBatchTransactionByIdAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<BatchTransactionViewModel>> GetBatchTransactionsByBankIdAsync(
           int bankId, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> LinkBankIdToBatchTransactionAsync(
            BatchLinkBankIdModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UnlinkBankIdFromBatchTransactionAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateBatchAdjustmentAsync(
           BatchAdjustmentCreateModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> SplitBatchTransactionAsync(
            BatchTransactionSplitModel model, CancellationToken cancellationToken = default);

        // CREDIT TRANSACTIONS
        Task<ResponseModel<List<CreditTransactionViewModel>>> GetCreditTransactionsByBankIdAsync(
           int bankId, CancellationToken cancellationToken = default);


        // DEPOSITS
        Task<ResponseModel<List<DepositViewModel>>> SearchDepositsAsync(
            DepositFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<DepositViewModel>> GetDepositByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<DepositViewModel>> GetDepositByBankIdAsync(
            int bankId, CancellationToken cancellationToken = default);
    }
}
