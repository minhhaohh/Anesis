namespace Anesis.Web.Data.Models
{
    public class ChangeLogViewModel
    {
        public string ActionName { get; set; }

        public DateTime ActionTime { get; set; }

        public string ChangedBy { get; set; }

        public string UserComment { get; set; }

        public string Notes { get; set; }

        public List<ChangedFieldViewModel> ChangedFields { get; set; }
    }

    public class ChangedFieldViewModel
    {
        public string FieldName { get; set; }

        public string BeforeChange { get; set; }

        public string AfterChange { get; set; }
    }
}
