using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFAQEnhanced.Areas.Identity.Data;

namespace MyFAQEnhanced.Areas.Identity.Data;

public class DBContextIdentity : IdentityDbContext<ApplicationUser>
{
    public DBContextIdentity(DbContextOptions<DBContextIdentity> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
