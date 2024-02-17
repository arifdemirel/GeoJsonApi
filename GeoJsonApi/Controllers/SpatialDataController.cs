using GeoJsonApi.Data;
using GeoJsonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
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

        private readonly static WKTReader _wktReader = new(new NtsGeometryServices(new PrecisionModel(), 4326));
        private readonly static WKTWriter _wktWriter = new();

        public SpatialDataController(SpatialDataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAll()
        {
            var spatialDatas = await _context.spatialdatas.ToListAsync();
                                                            //ToListAsync();
            var wktList = spatialDatas.Select(data => _wktWriter.Write(data.Geometry)).ToList();

            return Ok(wktList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Retrieve the spatial data entity by id
            var spatialData = await _context.spatialdatas.FindAsync(id);

            if (spatialData == null)
            {
                return NotFound();
            }

            // Convert the Geometry to WKT
            string wkt = _wktWriter.Write(spatialData.Geometry);

            return Ok(wkt);
        }


        [HttpPost]
        public IActionResult Post([FromBody] JsonElement jsonElement)
        {
            try
            {
                // Now _wktReader is accessible here
                if (jsonElement.TryGetProperty("wkt", out JsonElement wktElement) && wktElement.GetString() is string wkt)
                {
                    Geometry geometry = _wktReader.Read(wkt);
                    var spatialData = new SpatialData
                    {
                        Geometry = geometry
                    };

                    _context.spatialdatas.Add(spatialData);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return BadRequest("The 'wkt' field is required.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the spatial data by id
            var spatialData = await _context.spatialdatas.FindAsync(id);
            if (spatialData == null)
            {
                // If not found, return a 404 Not Found response
                return NotFound();
            }

            // If found, remove the spatial data from the context
            _context.spatialdatas.Remove(spatialData);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 200 OK response
            return Ok();
        }
    }
}

