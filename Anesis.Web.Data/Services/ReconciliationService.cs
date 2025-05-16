using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        // RECONCILIATIONS
        public async Task<ResponseModel<List<ReconciliationViewModel>>> GetAllReconciliationsAsync(
            CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<ReconciliationViewModel>>>(
                $"{API_Reconciliation}/All", cancellationToken);
        }

        public async Task<ResponseModel<ReconciliationViewModel>> GetLastTimeReconciliationAsync(
            CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<ReconciliationViewModel>>(
                $"{API_Reconciliation}/LastTime", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateReconciliationAsync(
           DateTime? reconciledDate, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Reconciliation}", reconciledDate, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UndoReconciliationAsync(
           int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"{API_Reconciliation}/{id}", cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        // BANK TRANSACTIONS
        public async Task<ResponseModel<List<BankTransactionViewModel>>> SearchBankTransactionsAsync(
            BankTransactionFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<BankTransactionViewModel>>>(
                $"{API_Reconciliation}/Banks?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<BankTransactionViewModel>> GetBankTransactionByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<BankTransactionViewModel>>(
                $"{API_Reconciliation}/Banks/{id}", cancellationToken);
        }

        public async Task<ResponseModel<BalanceSummaryViewModel>> GetBalanceSummary(
            BankTransactionFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<BalanceSummaryViewModel>>(
                $"{API_Reconciliation}/Banks/BalanceSummary?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateBankAdjustmentAsync(
           BankAdjustmentCreateModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Reconciliation}/Banks/Adjustments", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> SplitBankTransactionAsync(
            BankTransactionSplitModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{API_Reconciliation}/Banks/Split/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        // BATCH TRANSACTIONS
        public async Task<ResponseModel<List<BatchTransactionViewModel>>> SearchBatchTransactionsAsync(
            BatchTransactionFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<BatchTransactionViewModel>>>(
                $"{API_Reconciliation}/Batches?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<BatchTransactionViewModel>> GetBatchTransactionByIdAsync(
           int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<BatchTransactionViewModel>>(
                $"{API_Reconciliation}/Batches/{id}", cancellationToken);
        }

        public async Task<ResponseModel<BatchTransactionViewModel>> GetBatchTransactionsByBankIdAsync(
           int bankId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<BatchTransactionViewModel>>(
                $"{API_Reconciliation}/Batches/ByBankId/{bankId}", cancellationToken);
        }

        public async Task<ResponseModel<string>> LinkBankIdToBatchTransactionAsync(
            BatchLinkBankIdModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{API_Reconciliation}/Batches/LinkBankId/{model.BatchTransactionId}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UnlinkBankIdFromBatchTransactionAsync(
           int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsync(
                $"{API_Reconciliation}/Batches/UnlinkBankId/{id}", null, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> CreateBatchAdjustmentAsync(
           BatchAdjustmentCreateModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Reconciliation}/Batches/Adjustments", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> SplitBatchTransactionAsync(
            BatchTransactionSplitModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{API_Reconciliation}/Batches/Split/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        // CREDIT TRANSACTIONS
        public async Task<ResponseModel<List<CreditTransactionViewModel>>> GetCreditTransactionsByBankIdAsync(
           int bankId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CreditTransactionViewModel>>>(
                $"{API_Reconciliation}/Credits/ByBankId/{bankId}", cancellationToken);
        }

        // DEPOSITS
        public async Task<ResponseModel<List<DepositViewModel>>> SearchDepositsAsync(
            DepositFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<DepositViewModel>>>(
                $"{API_Reconciliation}/Deposits?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<DepositViewModel>> GetDepositByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<DepositViewModel>>(
                $"{API_Reconciliation}/Deposits/{id}", cancellationToken);
        }

        public async Task<ResponseModel<DepositViewModel>> GetDepositByBankIdAsync(
            int bankId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<DepositViewModel>>(
                $"{API_Reconciliation}/Deposits/ByBankId/{bankId}", cancellationToken);
        }
    }
}
