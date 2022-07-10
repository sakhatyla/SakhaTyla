using System.Text.RegularExpressions;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Web.Front.Models
{
    public class HtmlSection
    {
        public string? Html { get; set; }
        public WidgetModel? Widget { get; set; }
        public SystemWidget? SystemWidget { get; set; }

        public static List<HtmlSection> BreakToSections(string? html, List<WidgetModel> widgets, List<SystemWidget>? systemWidgets = null)
        {
            if (string.IsNullOrEmpty(html))
            {
                return new List<HtmlSection>();
            }
            var codePattern = "[a-zA-Z0-9_\\-]+";
            var sections = Regex.Split(html, $"(\\[\\[\\s*{codePattern}\\s*\\]\\])");
            return sections.Select(s =>
            {
                var m = Regex.Match(s, $"\\[\\[\\s*({codePattern})\\s*\\]\\]");
                if (m.Success)
                {
                    var code = m.Groups[1].Value;
                    if (systemWidgets != null)
                    {
                        var systemWidget = systemWidgets.FirstOrDefault(w => string.Equals(w.Code, code, StringComparison.InvariantCultureIgnoreCase));
                        if (systemWidget != null)
                        {
                            return new HtmlSection()
                            {
                                SystemWidget = systemWidget,
                            };
                        }
                    }
                    var widget = widgets.FirstOrDefault(w => string.Equals(w.Code, code, StringComparison.InvariantCultureIgnoreCase));
                    if (widget != null)
                    {
                        return new HtmlSection()
                        {
                            Widget = widget,
                        };
                    }

                }
                return new HtmlSection() { Html = s };
            })
                .ToList();
        }
    }
}
