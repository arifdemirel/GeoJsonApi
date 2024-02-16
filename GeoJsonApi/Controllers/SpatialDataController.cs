using GeoJsonApi.Data;
using GeoJsonApi.Models;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Text.Json;

namespace GeoJsonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpatialDataController : ControllerBase
    {
        private readonly SpatialDataContext _context;

        public SpatialDataController(SpatialDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement geoJson)
        {
            var geoJsonReader = new GeoJsonReader();
            //var geometry = geoJsonReader.Read<Geometry>(geoJson.GetRawText());
            Feature feature = geoJsonReader.Read<Feature>(geoJson.GetRawText());
            Geometry geometry = feature.Geometry;

            var spatialData = new SpatialData { Geometry = geometry };
            _context.SpatialDatas.Add(spatialData);
            _context.SaveChanges();

            return Ok();
        }
    }
}
