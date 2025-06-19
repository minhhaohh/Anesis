using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IMenuService
    {
        Task<ResponseModel<List<MenuItemViewModel>>> GetMenuItemsForCurrentUserAsync(CancellationToken cancellationToken = default);

        Task<ResponseModel<List<QuickLinkViewModel>>> GetQuickLinksForCurrentUserAsync(CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateQuickLinkAsync(QuickLinkEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateQuickLinkAsync(QuickLinkEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> DeleteQuickLinkAsync(int id, CancellationToken cancellationToken = default);
    }
}
