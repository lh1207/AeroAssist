using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AeroAssist.Data
{
    public class AeroAssistContext : IdentityDbContext<IdentityUser>
    {
        public AeroAssistContext(DbContextOptions<AeroAssistContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await Tickets.ToListAsync();
        }
    }
}