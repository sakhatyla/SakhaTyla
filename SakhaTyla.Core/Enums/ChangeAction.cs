using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SakhaTyla.Core.Enums
{
    public enum ChangeAction
    {
        [Description("Add")]
        Add = 0,
        [Description("Update")]
        Update = 1,
        [Description("Delete")]
        Delete = 2,
    }
}
