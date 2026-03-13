namespace SakhaTyla.Core.Requests.Public.SpellCheck.Models
{
    public class FixSpellingModel
    {
        public FixSpellingModel(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
