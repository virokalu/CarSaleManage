namespace CarSaleManage.Models.Dtos
{
    public class VehicleEditDto : VehicleDto
    {
        public int Id { get; set; }
        public List<string> ExistingImages { get; set; } = new();
        public List<string> ImagesToRemove { get; set; } = new();
    }
}
