using Microsoft.AspNetCore.Identity;

namespace GeoJsonApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
