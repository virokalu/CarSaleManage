using System.ComponentModel.DataAnnotations;

namespace CarSaleManagement.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string? Make { get; set; }
    public string ModelNo { get; set; }
    public string? Classification { get; set; }
    public string Origin { get; set; }
    public string UsedCountry { get; set; }
    public int Year { get; set; }
    public string? RegNo { get; set; }
    [DataType(DataType.Date)]
    public DateTime? RegDate { get; set; }
    public string EngineNo { get; set; }
    public string? FuelSystem { get; set; }
    public int EngineCap { get; set; }
    public string ChassisNo { get; set; }
    public string FuelType { get; set; }
    public string Color { get; set; }
    public int? MeterReading { get; set; }

}