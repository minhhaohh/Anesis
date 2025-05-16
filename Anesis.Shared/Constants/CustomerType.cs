using System.ComponentModel;

namespace Anesis.Shared.Constants
{
    public enum CustomerType
    {
        [Description("Patients")]
        Patients = 1,

        [Description("Vendors / Suppliers")]
        Suppliers = 2,

        [Description("Partners")]
        Partners = 4,
    }
}
