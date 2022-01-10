using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ActivityPlanner.Entities.Enums
{
    public enum StatusType
    {
        [Description("Active")]
        Active = 1,
        [Description("Disabled")]
        Disabled = 2,
        [Description("Cancelled")]
        Cancelled = 3,
        [Description("Done")]
        Done = 4
    }
}
