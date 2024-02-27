﻿using WktApi.Data;
using WktApi.Models;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WktApi.Extension;
using WktApi.Models.Dto;


namespace WktApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpatialDataController : ControllerBase
    {
        private readonly SDContext _context;

        public SpatialDataController(SDContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddGeometry([FromBody] GeomInputModel inputModel)
        {


            if (inputModel == null || string.IsNullOrWhiteSpace(inputModel.Wkt))
            {
                return BadRequest("Invalid input");
            }

            var geometry = inputModel.Wkt.WktToGeometry();
            if (geometry == null)
            {
                return BadRequest("Invalid WKT string");
            }

            var spatialData = new SpatialData
            {

                Geometry = geometry
            };


            _context.spatialdatas.Add(spatialData);
            await _context.SaveChangesAsync();

            return Ok(spatialData);
        }

        [HttpPost("AddSampleGeometries")]
        public async Task<IActionResult> AddSampleGeometries()
        {

            var pointWkt = "POINT (30 10)";
            var pointGeometry = pointWkt.WktToGeometry();

            var pointSpatialData = new SpatialData
            {
                Geometry = pointGeometry
            };

            _context.spatialdatas.Add(pointSpatialData);


            var polygonWkt = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))";
            var polygonGeometry = polygonWkt.WktToGeometry();

            var polygonSpatialData = new SpatialData
            {
                Geometry = polygonGeometry
            };

            _context.spatialdatas.Add(polygonSpatialData);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Sample geometries added successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var geometries = await _context.spatialdatas.Select(s => new GeomOutputModel
            {
                Id = s.Id,
                Wkt = s.Geometry.ToText()
            })
                .ToListAsync();

            return Ok(geometries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var geometry = await _context.spatialdatas
                .Where(s => s.Id == id)
                .Select(s => new GeomOutputModel
                {
                    Id = s.Id,
                    Wkt = s.Geometry
                .ToText()
                })
                .FirstOrDefaultAsync();

            if (geometry == null)
            {
                return NotFound($"Geometry with the ID: {id} not found!");
            }

            return Ok(geometry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var spatialData = await _context.spatialdatas.FindAsync(id);
            if (spatialData == null)
            {
                return NotFound(new { message = $"Geometry with ID {id} was not found." });
            }

            _context.spatialdatas.Remove(spatialData);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Geometry with Id:{id} was successfully deleted." });
        }

    }
}

