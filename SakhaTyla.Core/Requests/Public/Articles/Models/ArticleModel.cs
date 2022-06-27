
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Requests.Public.Articles.Models
{
    public class ArticleModel
    {
        public ArticleModel(string title, string text)
        {
            Title = title;
            Text = text;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Text")]
        public string Text { get; set; }

        [DisplayName("From Language")]
        public Languages.Models.LanguageShortModel FromLanguage { get; set; } = null!;

        [DisplayName("To Language")]
        public Languages.Models.LanguageShortModel ToLanguage { get; set; } = null!;

        [DisplayName("Category")]
        public Categories.Models.CategoryShortModel? Category { get; set; }
    }
}
