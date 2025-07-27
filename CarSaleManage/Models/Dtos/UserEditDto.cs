namespace CarSaleManage.Models.Dtos
{
    public class UserEditDto
    {
        public int Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
