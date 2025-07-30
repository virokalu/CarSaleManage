using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarSaleManage.Models.Services;
using CarSaleManage.Models.Dtos;

namespace CarSaleManage.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _vehicleService.ListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _vehicleService.FindByIdAsync(id.Value);
            if (result.Success) return View(result.Data);

            Console.WriteLine(result.Error);
            return NotFound();
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleDto vehicle)
        {
            if (ModelState.IsValid)
            {
                var result = await _vehicleService.Save(vehicle);
                if (result.Success) return RedirectToAction(nameof(Index));

                return View(vehicle);              
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _vehicleService.FindByIdAsync(id.Value);
            if (result.Success)
            {
                var vehicle = result.Data;
                if (vehicle != null)
                {
                    var vehicleEditdto = new VehicleEditDto
                    {
                        Id = vehicle.Id,
                        Make = vehicle.Make,
                        ModelNo = vehicle.ModelNo,
                        Classification = vehicle.Classification,
                        Origin = vehicle.Origin,
                        UsedCountry = vehicle.UsedCountry,
                        Year = vehicle.Year,
                        RegNo = vehicle.RegNo,
                        RegDate = vehicle.RegDate == null? new DateTime() : vehicle.RegDate.Value,
                        EngineNo = vehicle.EngineNo,
                        FuelSystem = vehicle.FuelSystem,
                        EngineCap = vehicle.EngineCap,
                        ChassisNo = vehicle.ChassisNo,
                        FuelType = vehicle.FuelType,
                        Color = vehicle.Color,
                        MeterReading = vehicle.MeterReading==null? 0 : vehicle.MeterReading.Value,
                        ExistingImages = vehicle.Images
                    };
                    return View(vehicleEditdto);
                }
            }
            return NotFound();
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleEditDto vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }
            if ((vehicle.ExistingImages?.Count ?? 0) - (vehicle.ImagesToRemove?.Count ?? 0) + (vehicle.Images?.Count ?? 0) < 1)
            {
                ModelState.AddModelError("", "At least one image is required.");
                return View(vehicle);
            }

            if (ModelState.IsValid)
            {
                var result = await _vehicleService.Update(vehicle);
                if(result.Success) return RedirectToAction(nameof(Index));

                NotFound();
                Console.WriteLine(result.Error);
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _vehicleService.FindByIdAsync(id.Value);
            if (result.Success) return View(result.Data);

            Console.WriteLine(result.Error);
            return NotFound();
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _vehicleService.Delete(id);
            if (result.Success) return RedirectToAction(nameof(Index));

            Console.WriteLine(result.Error);
            return NotFound();
        }
    }
}
