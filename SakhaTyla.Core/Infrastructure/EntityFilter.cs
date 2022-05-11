namespace SakhaTyla.Core.Infrastructure
{
    public class EntityFilter
    {
        public EntityFilter()
        {
        }

        public EntityFilter(string text)
        {
            Text = text;
        }

        public string? Text { get; set; }
    }
}
