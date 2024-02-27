using System.ComponentModel.DataAnnotations;

namespace WktApi.Models.Dto
{
    public class SpatialDataRequest
    {
        [Required(ErrorMessage = "WKT string is required.")]
        public string Wkt { get; set; }
    }
}
