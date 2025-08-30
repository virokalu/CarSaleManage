namespace CarSaleManage.Models.Dtos
{
    public class UserEditDto
    {
        public required string Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? IdNumber { get; set; }
        public string? Address { get; set; }
    }
}
