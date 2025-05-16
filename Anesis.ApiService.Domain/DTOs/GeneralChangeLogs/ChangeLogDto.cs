namespace Anesis.ApiService.Domain.DTOs.GeneralChangeLogs
{
    public class ChangeLogDto
    {
        public int Id { get; set; }

        public string ActionName { get; set; }

        public DateTime ActionTime { get; set; }

        public string ChangedBy { get; set; }

        public string UserComment { get; set; }

        public string Notes { get; set; }

        public List<ChangedFieldDto> ChangedFields { get; set; }
    }
}
