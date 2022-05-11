using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Articles.Models
{
    public class ArticleModel
    {
        public ArticleModel(string title, string text, string textSource)
        {
            Title = title;
            Text = text;
            TextSource = textSource;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel? CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel? ModificationUser { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Text")]
        public string Text { get; set; }

        [DisplayName("Text Source")]
        public string TextSource { get; set; }

        [DisplayName("From Language")]
        public int FromLanguageId { get; set; }
        public Languages.Models.LanguageShortModel FromLanguage { get; set; } = null!;

        [DisplayName("To Language")]
        public int ToLanguageId { get; set; }
        public Languages.Models.LanguageShortModel ToLanguage { get; set; } = null!;

        [DisplayName("Fuzzy")]
        public bool Fuzzy { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        public Categories.Models.CategoryShortModel? Category { get; set; }
    }
}
