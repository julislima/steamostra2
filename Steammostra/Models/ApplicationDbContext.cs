using Microsoft.EntityFrameworkCore;
namespace Steammostra.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
        options) : base(options)
        {
        }
        public DbSet<Logins> Logins { get; set; }
        internal bool TestConnection()
        {
            throw new NotImplementedException();
        }
    }
}