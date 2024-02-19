using GeoJsonApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoJsonApi.Data
{
    public class SpatialDataContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public SpatialDataContext(DbContextOptions<SpatialDataContext> options) : base(options)
        {

        }

#pragma warning disable IDE1006 // Naming Styles - For SpatialData in PostGreSql, lowercase seems to be the norm or so I have been told.
        public DbSet<SpatialData> spatialdatas { get; set; }
#pragma warning restore IDE1006 // Naming Styles
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }

    }
}
