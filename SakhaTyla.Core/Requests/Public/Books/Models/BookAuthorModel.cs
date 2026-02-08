namespace SakhaTyla.Core.Requests.Public.Books.Models
{
    public class BookAuthorModel
    {
        public BookAuthorModel(string lastName)
        {
            LastName = lastName;
        }

        public int Id { get; set; }

        public string LastName { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string GetFullName()
        {
            return LastName + " "
                + (!string.IsNullOrEmpty(FirstName) ? FirstName[0] + "." : "")
                + (!string.IsNullOrEmpty(MiddleName) ? MiddleName[0] + "." : "");
        }
    }
}
