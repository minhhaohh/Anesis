namespace Anesis.ApiService.Domain.DTOs.GeneralChangeLogs
{
    public class ChangedFieldDto
    {
        public string FieldName { get; set; }

        public string BeforeChange { get; set; }

        public string AfterChange { get; set; }
    }
}
