using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace WktApi.Extensions
{
    public static class GeometryExtensions
    {

        private readonly static WKTReader WktReader = new(new NtsGeometryServices(new PrecisionModel(), 4326));


        /// <summary>
        /// Converts a Well-Known Text (WKT) string to a Geometry object.
        /// </summary>
        /// <param name="wkt">The WKT string to convert.</param>
        /// <returns>The Geometry object represented by the WKT string, or null if the conversion fails.</returns>
        public static Geometry WktToGeometry(this string wkt)
        {
            try
            {
                return WktReader.Read(wkt);
            }
            catch (Exception ex)
            {


                Console.WriteLine($"Error converting WKT to Geometry: {ex.Message}");
                return null;
            }
        }
    }
}
