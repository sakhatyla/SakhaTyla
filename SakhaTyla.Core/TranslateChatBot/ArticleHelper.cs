using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.XSS;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Core.TranslateChatBot
{
    public static class ArticleHelper
    {
        private static readonly HtmlSanitizer _htmlSanitizer = InitSanitizer();
        private const string ArrowRight = "➡️";

        public static string GetPreparedText(this ArticleModel article)
        {
            var text = $"{article.FromLanguage.Name} {ArrowRight} {article.ToLanguage.Name}\n<b>{article.Title}</b>\n{PrepareHtmlText(article.Text)}";
            if (article.Category != null)
            {
                text += $"\n\n<em>Категория: {article.Category.Name}</em>";
            }                
            return text;
        }

        private static string PrepareHtmlText(string text)
        {
            return _htmlSanitizer.Sanitize(text);
        }
        
        private static HtmlSanitizer InitSanitizer()
        {
            var sanitizer = new HtmlSanitizer();
            sanitizer.KeepChildNodes = true;
            sanitizer.AllowedTags.Clear();
            sanitizer.AllowedTags.Add("b");
            sanitizer.AllowedTags.Add("strong");
            sanitizer.AllowedTags.Add("i");
            sanitizer.AllowedTags.Add("em");
            sanitizer.AllowedAttributes.Clear();
            return sanitizer;
        }
    }
}
