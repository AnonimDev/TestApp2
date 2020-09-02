using Microsoft.EntityFrameworkCore;

namespace TestApp2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BaseModel> BaseModel { get; set; }
    }
}
