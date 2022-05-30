using System;
using System.ComponentModel;

namespace SakhaTyla.Core.Enums
{
    public enum PageType
    {
        [Description("General")]
        General = 0,
        [Description("Blog")]
        Blog = 1,
        [Description("Article")]
        Article = 2,
        [Description("Main")]
        Main = 3
    }
}
