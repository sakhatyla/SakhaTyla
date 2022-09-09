namespace SakhaTyla.Web.Front.Legacy.Models
{
    public class TranslateModel
    {
        public TranslateModel(string query)
        {
            Query = query;
        }

        public string Query { get; set; }

        public List<ArticleGroupModel> Articles { get; set; } = null!;

        public List<ArticleModel>? MoreArticles { get; set; }
    }
}
