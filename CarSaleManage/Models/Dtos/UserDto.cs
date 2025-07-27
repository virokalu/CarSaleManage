namespace CarSaleManage.Models.Dtos
{
    public class UserDto
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
