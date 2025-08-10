using CarSaleManage.Models;
using CarSaleManage.Models.Dtos;
using CarSaleManage.Models.Repositories;
using CarSaleManage.Models.Services;
using CarSaleManage.Models.Services.Communication;
using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly UserManager<AppUser> _userManager;

        public VehicleService(IVehicleRepository vehicleRepository, UserManager<AppUser> userManager)
        {
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
        }

        public async Task<ServiceResult<Vehicle>> Delete(int id)
        {
            try
            {
                var vehicle = await _vehicleRepository.FindByIdAsync(id);
                if (vehicle == null)
                {
                    return ServiceResult<Vehicle>.Fail("Vehicle not Found");
                }
                else
                {
                    _vehicleRepository.Remove(vehicle);
                    await _vehicleRepository.CompleteAsync();
                    return ServiceResult<Vehicle>.Ok();
                }

            }
            catch (Exception ex)
            {
                return ServiceResult<Vehicle>.Fail(ex.Message);
            }

        }

        public async Task<ServiceResult<Vehicle>> FindByIdAsync(int id)
        {
            try
            {
                var vehicle = await _vehicleRepository.FIndByIdWithVehicleAsync(id);
                if (vehicle == null)
                {
                    return ServiceResult<Vehicle>.Fail("Vehicle not Found");
                }
                else
                {
                    return ServiceResult<Vehicle>.Ok(vehicle);
                }
            }catch (Exception ex)
            {
                return ServiceResult<Vehicle>.Fail(ex.Message);
            }
        }

        public async Task<IEnumerable<Vehicle>> ListAsync()
        {
            return await _vehicleRepository.ListAsync();
        }

        public async Task<ServiceResult<Vehicle>> Save(VehicleDto vehicleDto)
        {
            List<string> imagePaths = await ImageStore(vehicleDto.Images);

            var vehicle = new Vehicle
            {
                Make = vehicleDto.Make,
                ModelNo = vehicleDto.ModelNo,
                Classification = vehicleDto.Classification,
                Origin = vehicleDto.Origin,
                UsedCountry = vehicleDto.UsedCountry,
                Year = vehicleDto.Year,
                RegNo = vehicleDto.RegNo,
                RegDate = vehicleDto.RegDate,
                EngineNo = vehicleDto.EngineNo,
                FuelSystem = vehicleDto.FuelSystem,
                EngineCap = vehicleDto.EngineCap,
                ChassisNo = vehicleDto.ChassisNo,
                FuelType = vehicleDto.FuelType,
                Color = vehicleDto.Color,
                MeterReading = vehicleDto.MeterReading,
                Images = imagePaths
            };

            await _vehicleRepository.AddAsync(vehicle);
            await _vehicleRepository.CompleteAsync();
            return ServiceResult<Vehicle>.Ok();
        }

        private static async Task<List<string>> ImageStore(List<IFormFile> Images)
        {
            var imagePaths = new List<string>();
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            foreach (var formFile in Images)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Store the relative path to the image
                    imagePaths.Add("/uploads/" + fileName);
                }
            }

            return imagePaths;
        }

        public async Task<ServiceResult<Vehicle>> Update(VehicleEditDto vehicleEditDto)
        {
            try
            {
                var vehicle = await _vehicleRepository.FindByIdAsync(vehicleEditDto.Id);

                if (vehicle == null) return ServiceResult<Vehicle>.Fail("Vehicle not Found");

                vehicle.Images = vehicle.Images
                    .Except(vehicleEditDto.ImagesToRemove ?? new List<string>())
                    .ToList();

                vehicle.Make = vehicleEditDto.Make;
                vehicle.ModelNo = vehicleEditDto.ModelNo;
                vehicle.Classification = vehicleEditDto.Classification;
                vehicle.Origin = vehicleEditDto.Origin;
                vehicle.UsedCountry = vehicleEditDto.UsedCountry;
                vehicle.Year = vehicleEditDto.Year;
                vehicle.RegNo = vehicleEditDto.RegNo;
                vehicle.RegDate = vehicleEditDto.RegDate;
                vehicle.EngineNo = vehicleEditDto.EngineNo;
                vehicle.FuelSystem = vehicleEditDto.FuelSystem;
                vehicle.EngineCap = vehicleEditDto.EngineCap;
                vehicle.ChassisNo = vehicleEditDto.ChassisNo;
                vehicle.FuelType = vehicleEditDto.FuelType;
                vehicle.Color = vehicleEditDto.Color;
                vehicle.MeterReading = vehicleEditDto.MeterReading;

                List<string> imagePaths = vehicleEditDto.Images==null ? new() : await ImageStore(vehicleEditDto.Images);

                vehicle.Images.AddRange(imagePaths);

                _vehicleRepository.Update(vehicle);
                await _vehicleRepository.CompleteAsync();
                return ServiceResult<Vehicle>.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult<Vehicle>.Fail(ex.Message);
            }
        }
            
        public async Task<ServiceResult<Vehicle>> Buy(int id, string userId)
        {
            try
            {
                var vehicle = await _vehicleRepository.FIndByIdWithVehicleAsync(id);
                var user = await _userManager.FindByIdAsync(userId);
                if (vehicle != null && user != null)
                {
                    vehicle.AppUserId = userId;
                    vehicle.PurchaseDate = DateTime.Now;
                    await _vehicleRepository.CompleteAsync();
                    return ServiceResult<Vehicle>.Ok(vehicle);
                }
                return ServiceResult<Vehicle>.Fail("User or vehicle not found!");
            }
            catch (Exception ex) 
            {
                return ServiceResult<Vehicle>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<Vehicle>> RemoveBuy(int id)
        {
            try
            {
                var vehicle = await _vehicleRepository.FIndByIdWithVehicleAsync(id);
                if (vehicle != null)
                {
                    vehicle.AppUserId = null;
                    vehicle.PurchaseDate = null;
                    await _vehicleRepository.CompleteAsync();
                    return ServiceResult<Vehicle>.Ok(vehicle);
                }
                return ServiceResult<Vehicle>.Fail("Vehicle not found!");
            }
            catch(Exception ex)
            {
                return ServiceResult<Vehicle>.Fail(ex.Message);
            }
        }

        public Task<IEnumerable<Vehicle>> SearchListAsync(string? searchString)
        {
            if (searchString == null)
            {
                return _vehicleRepository.ListAsync();
            }
            else
            {
                int intValue;
                int.TryParse(searchString, out intValue);
                return _vehicleRepository.SearchListAsync(searchString, intValue);
            }
        }
    }
}
