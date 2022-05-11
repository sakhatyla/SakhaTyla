namespace SakhaTyla.Infrastructure.Email
{
    public class EmailSenderOptions
    {
        public string? PickupFolder { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string? DefaultFrom { get; set; }
    }
}
