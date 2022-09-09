namespace SakhaTyla.Web.Front.Legacy.Models
{
    public class ArticleModel
    {
        public ArticleModel(string title, string text, string fromLanguageName, string toLanguageName)
        {
            Title = title;
            Text = text;
            FromLanguageName = fromLanguageName;
            ToLanguageName = toLanguageName;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string FromLanguageName { get; set; }

        public string ToLanguageName { get; set; }

        public string? CategoryName { get; set; }
    }
}
