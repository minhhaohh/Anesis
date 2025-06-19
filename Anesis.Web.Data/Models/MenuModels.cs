namespace Anesis.Web.Data.Models
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }

        public int MenuTabId { get; set; }

        public string TabName { get; set; }

        public int TabDisplayOrder { get; set; }

        public string TabIconPath { get; set; }

        public string MenuText { get; set; }

        public string LinkUrl { get; set; }

        public string Tooltip { get; set; }

        public string IconPath { get; set; }

        public string CssClass { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class QuickLinkViewModel
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public string MenuText { get; set; }

        public string LinkUrl { get; set; }

        public string Tooltip { get; set; }

        public string IconPath { get; set; }

        public string CssClass { get; set; }

        public int DisplayOrder { get; set; }

        public string GroupName { get; set; }

        public QuickLinkEditModel ToEditModel()
        {
            return new QuickLinkEditModel
            {
                Id = Id,
                MenuItemId = MenuItemId,
                DisplayOrder = DisplayOrder,
                GroupName = GroupName
            };
        }
    }

    public class QuickLinkEditModel
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public int DisplayOrder { get; set; }

        public string GroupName { get; set; }
    }
}
