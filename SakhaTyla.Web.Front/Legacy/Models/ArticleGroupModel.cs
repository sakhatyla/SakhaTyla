namespace SakhaTyla.Web.Front.Legacy.Models
{
    public class ArticleGroupModel
    {
        public ArticleGroupModel(string fromLanguageName, string toLanguageName)
        {
            FromLanguageName = fromLanguageName;
            ToLanguageName = toLanguageName;
        }

        public string FromLanguageName { get; set; }

        public string ToLanguageName { get; set; }

        public List<ArticleModel> Articles { get; set; } = null!;
    }
}
