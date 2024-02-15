using GeoJsonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoJsonApi.Data
{
    public class GeoJsonDbContext : DbContext
    {
        public GeoJsonDbContext(DbContextOptions<GeoJsonDbContext> options) : base(options)
        {

        }
        //public DbSet<Point> Points { get; set; }

        public DbSet<PointEntity> PointEntities { get; set; }

        public DbSet<LineStringEntity> LineStringEntities { get; set; }

        public DbSet<PolygonEntity> PolygonEntities { get; set; }

        public DbSet<MultiPointEntity> MultiPointEntities { get; set; }

        public DbSet<MultiLineStringEntity> MultiLineStringEntities { get; set; }

        public DbSet<MultiPolygonEntity> MultiPolygonEntities { get; set; }

        public DbSet<GeometryCollectionEntity> GeometryCollectionEntities { get; set; }


    }
}
