using NetTopologySuite.Geometries;

namespace GeoJsonApi.Models.BaseModel
{
    public abstract class GeometryEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Geometry? Geometry { get; set; }
    }
}
