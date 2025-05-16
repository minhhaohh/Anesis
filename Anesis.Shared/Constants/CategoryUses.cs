using System.ComponentModel;

namespace Anesis.Shared.Constants
{
    public enum CategoryUses
    {
        [Description("General purpose")]
        Unknown = 0,

        [Description("Manual Document Categories")]
        ManualDocCategory = 1,

        [Description("Manual Document Topics")]
        ManualDocTopic = 2,

        [Description("USA Counties")]
        UsaCounty = 3
    }
}
