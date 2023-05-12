using Microsoft.EntityFrameworkCore;

namespace KFCSharedData
{
    public class KFCDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; } = null!;

        public KFCDbContext(DbContextOptions<KFCDbContext> options) : base(options)
        {
            
        }
    }
}
