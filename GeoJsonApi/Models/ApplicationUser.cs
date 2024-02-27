using Microsoft.AspNetCore.Identity;

namespace WktApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
