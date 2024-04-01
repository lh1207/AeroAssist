using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AeroAssist.DB
{
    public class AeroAssistContext : IdentityDbContext<IdentityUser>
    {
        public AeroAssistContext(DbContextOptions<AeroAssistContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}