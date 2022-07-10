namespace SakhaTyla.Web.Front.Models
{
    public class SystemWidget
    {
        public SystemWidget(string code, string viewName, Func<object>? getModel = null)
        {
            Code = code;
            ViewName = viewName;
            GetModel = getModel;
        }

        public string Code { get; set; }
        public string ViewName { get; set; }
        public Func<object>? GetModel { get; set; }
    }
}
