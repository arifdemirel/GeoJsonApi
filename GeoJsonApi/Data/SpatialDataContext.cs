using GeoJsonApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoJsonApi.Data
{
    public class SpatialDataContext : IdentityDbContext
    {
        public SpatialDataContext(DbContextOptions<SpatialDataContext> options) : base(options)
        {

        }

        public DbSet<SpatialData> spatialdatas { get; set; }

        
    }
}
