using NetTopologySuite.Geometries;

namespace WktApi.Models
{
    public class SpatialData
    {
        public int Id { get; set; }        
        public Geometry Geometry { get; set; }

    }
}
