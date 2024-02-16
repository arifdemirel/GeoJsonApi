using GeoJsonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoJsonApi.Data
{
    public class SpatialDataContext : DbContext
    {
        public SpatialDataContext(DbContextOptions<SpatialDataContext> options) : base(options)
        {

        }

        public DbSet<SpatialData> SpatialDatas { get; set; }


    }
}
