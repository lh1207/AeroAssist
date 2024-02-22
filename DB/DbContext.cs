using Microsoft.EntityFrameworkCore;

namespace AeroAssist.DB
{
    public class AeroAssistContext : DbContext
    {
        public AeroAssistContext(DbContextOptions<AeroAssistContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}