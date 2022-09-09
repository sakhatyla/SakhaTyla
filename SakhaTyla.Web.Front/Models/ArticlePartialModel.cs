using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Web.Front.Models
{
    public class ArticlePartialModel
    {
        public ArticlePartialModel(ArticleModel article)
        {
            Article = article;
        }

        public ArticleModel Article { get; set; }
        public bool ShowLanguage { get; set; }
    }
}
