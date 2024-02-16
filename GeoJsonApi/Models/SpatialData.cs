using NetTopologySuite.Geometries;

namespace GeoJsonApi.Models
{
    public class SpatialData
    {
        public int Id { get; set; }        
        public Geometry Geometry { get; set; }

    }
}
