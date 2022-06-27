using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Core.Indexers
{
    public static class ArticleSearch
    {
        public static readonly string DocumentType = "article";
        public static readonly string TitleField = "title";
        public static readonly string TextField = "text";
        public static readonly string TextSourceFromField = "textSourceFrom";
        public static readonly string TextSourceToField = "textSourceTo";
        public static readonly string FromLanguageNameField = "fromLanguageName";
        public static readonly string FromLanguageCodeField = "fromLanguageCode";
        public static readonly string ToLanguageNameField = "toLanguageName";
        public static readonly string ToLanguageCodeField = "toLanguageCode";
        public static readonly string CategoryNameField = "categoryName";

        public static Article GetArticle(IndexedDocument document)
        {
            var id = document.Id!.Split(':')![1];
            var title = document.Fields[TitleField].Value;
            var text = document.Fields[TextField].Value;
            var fromLanguageName = document.Fields[FromLanguageNameField].Value;
            var toLanguageName = document.Fields[ToLanguageNameField].Value;
            var categoryName = document.Fields[CategoryNameField].Value;
            var article = new Article(title, "")
            {
                Id = int.Parse(id),
                Text = text,
                FromLanguage = new Language(fromLanguageName, ""),
                ToLanguage = new Language(toLanguageName, ""),
                Category = !string.IsNullOrEmpty(categoryName) ? new Category(categoryName) : null,
            };
            return article;
        }

        public static Document GetDocument(Article article)
        {
            var document = new Document($"{DocumentType}:{article.Id}")
            {
                Type = DocumentType,
                Language = article.FromLanguage.Code,
            };
            document.Fields[TitleField] = new DocumentField(article.Title); // TODO: use standard analyzer?
            document.Fields[TextField] = new DocumentField(article.Text, analyzed: false);
            document.Fields[TextSourceFromField] = new DocumentField(article.TextSource, stored: false);
            document.Fields[TextSourceToField] = new DocumentField(article.TextSource, stored: false) { Language = article.ToLanguage.Code };
            document.Fields[FromLanguageNameField] = new DocumentField(article.FromLanguage.Name, analyzed: false);
            document.Fields[FromLanguageCodeField] = new DocumentField(article.FromLanguage.Code, stored: false, analyzed: false);
            document.Fields[ToLanguageNameField] = new DocumentField(article.ToLanguage.Name, analyzed: false);
            document.Fields[ToLanguageCodeField] = new DocumentField(article.ToLanguage.Code, stored: false, analyzed: false);
            document.Fields[CategoryNameField] = new DocumentField(article.Category?.Name ?? "", analyzed: false);
            return document;
        }

        public static List<string> GetLanguages(string query)
        {
            var result = new List<string>();
            if (Regex.IsMatch(query, "[a-z]", RegexOptions.IgnoreCase))
            {
                result.Add("en");
            }
            if (Regex.IsMatch(query, "[а-яё]", RegexOptions.IgnoreCase))
            {
                result.Add("ru");
            }
            if (Regex.IsMatch(query, "[а-яҥҕөһү]", RegexOptions.IgnoreCase))
            {
                result.Add("sah");
            }
            return result;
        }
    }
}
