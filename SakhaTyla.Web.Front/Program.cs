namespace SakhaTyla.Web.Front
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureLogging(logging =>
                        {
                            logging.AddSimpleConsole(c =>
                            {
                                c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
                            });
                        });
                });
    }
}
