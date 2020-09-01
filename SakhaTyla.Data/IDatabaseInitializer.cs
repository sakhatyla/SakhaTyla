using System.Threading.Tasks;

namespace SakhaTyla.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
