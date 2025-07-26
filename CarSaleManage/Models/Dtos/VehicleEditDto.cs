namespace CarSaleManage.Models.Dtos
{
    public class VehicleEditDto : VehicleDto
    {
        public int Id { get; set; }
        public List<String> ExistingImages { get; set; } = new List<string>();
    }
}
