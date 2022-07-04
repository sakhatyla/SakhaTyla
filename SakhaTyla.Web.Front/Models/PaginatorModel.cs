namespace SakhaTyla.Web.Front.Models
{
    public class PaginatorModel
    {
        public PaginatorModel(Func<int, string> generatePageUrl)
        {
            GeneratePageUrl = generatePageUrl;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Length { get; set; }
        public Func<int, string> GeneratePageUrl { get; set; }
    }
}
