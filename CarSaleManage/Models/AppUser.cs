using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Models
{
    public class AppUser : IdentityUser
    {
        public required string Firstname {  get; set; }
        public required string Lastname { get; set; }
        public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
    }
}
