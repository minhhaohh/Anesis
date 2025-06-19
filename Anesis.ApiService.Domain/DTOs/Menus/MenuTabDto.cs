namespace Anesis.ApiService.Domain.DTOs.Menus
{
    public class MenuTabDto
    {
        public int Id { get; set; }

        public string TabName { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool Visible { get; set; }

        public string IconPath { get; set; }

        public string Notes { get; set; }
    }
}
