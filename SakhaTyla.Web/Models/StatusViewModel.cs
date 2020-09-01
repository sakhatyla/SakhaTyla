namespace SakhaTyla.Web.Models
{
    public class StatusViewModel
    {
    }

    public class CreationStatusViewModel : StatusViewModel
    {
        public CreationStatusViewModel(int id)
        {
            Id = id;
        }

        public int? Id { get; set; }
    }
}
