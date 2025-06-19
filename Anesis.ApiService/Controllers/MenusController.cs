using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Menus;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class MenusController : ControllerBase
    {
        private readonly ILogger<MenusController> _logger;
        private readonly IMenuService _menuService;

        public MenusController(
            ILogger<MenusController> logger,
            IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        [HttpGet("MenuItems/ForCurrentUser")]
        public async Task<Result> GetMenuItemsForCurrentUser()
        {
            var data = await _menuService.GetMenusForUserAsync("754aa5fa-63a9-4986-9ba0-aa44f9b887e9");

            return Result.Ok(data);
        }

        [HttpGet("QuickLinks/ForCurrentUser")]
        public async Task<Result> GetQuickLinksForCurrentUser()
        {
            var data = await _menuService.GetQuickLinksForUserAsync("754aa5fa-63a9-4986-9ba0-aa44f9b887e9");

            return Result.Ok(data);
        }

        [HttpPost("QuickLinks")]
        public async Task<Result> CreateQuickLink([FromBody] QuickLinkEditDto model)
        {
            model.UserId = "754aa5fa-63a9-4986-9ba0-aa44f9b887e9";

            if (!await _menuService.CreateQuickLinkAsync(model))
            {
                return Result.Error("Something went wrong when creating quick link. Please try again.");
            }

            return Result.Ok($"Created new quick link successful.");
        }

        [HttpPut("QuickLinks/{id}")]
        public async Task<Result> UpdateQuickLink(int id, QuickLinkEditDto model)
        {
            // Make sure quick link ID is from the route
            model.Id = id;
            model.UserId = "754aa5fa-63a9-4986-9ba0-aa44f9b887e9";

            if (!await _menuService.UpdateQuickLinkAsync(model))
            {
                return Result.Error("Something went wrong when updating quick link. Please try again.");
            }

            return Result.Ok($"Updated quick link #{model.Id} successful.");
        }

        [HttpDelete("QuickLinks/{id}")]
        public async Task<Result> DeleteQuickLink([FromRoute] int id)
        {
            if (!await _menuService.DeleteQuickLinkAsync(id))
            {
                return Result.Error($"Something went wrong when deleting quick link #{id}. Please try again.");
            }

            return Result.Ok($"Deleted quick link #{id} successful.");
        }
    }
}
