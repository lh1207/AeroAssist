using Microsoft.EntityFrameworkCore;

namespace AeroAssist.DB
{
    public class AeroAssistContext(DbContextOptions<AeroAssistContext> options, DbSet<Ticket> tickets)
        : DbContext(options)
    {
        public DbSet<Ticket> Tickets { get; set; } = tickets;
    }
}