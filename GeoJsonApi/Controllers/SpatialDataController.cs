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
            var geoJsonText = geoJson.GetRawText();
            //var geometry = geoJsonReader.Read<Geometry>(geoJson.GetRawText());
            //Feature feature = geoJsonReader.Read<Feature>(geoJson.GetRawText());
            FeatureCollection featureCollection = geoJsonReader.Read<FeatureCollection>(geoJsonText);

            List<SpatialData> spatialDataList = new List<SpatialData>();

            foreach (var feature in featureCollection) 
            {
                Geometry geometry = feature.Geometry;

                SpatialData spatialData = new SpatialData
                {
                    Geometry = geometry
                    // Populate other properties as necessary
                };
                spatialDataList.Add(spatialData);
            }

            //var spatialData = new SpatialData { Geometry = geometry };
            //_context.SpatialDatas.Add(spatialData);
            //_context.SaveChanges();

            foreach (var spatialData in spatialDataList)
            {
                _context.SpatialDatas.Add(spatialData);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
