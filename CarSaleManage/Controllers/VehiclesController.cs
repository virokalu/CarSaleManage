using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarSaleManage.Data;
using CarSaleManage.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarSaleManage.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
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
        public async Task<IActionResult> Create([Bind("Id,Make,ModelNo,Classification,Origin,UsedCountry,Year,RegNo,RegDate,EngineNo,FuelSystem,EngineCap,ChassisNo,FuelType,Color,MeterReading")] Vehicle vehicle, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
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

                vehicle.Images = imagePaths;

                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,ModelNo,Classification,Origin,UsedCountry,Year,RegNo,RegDate,EngineNo,FuelSystem,EngineCap,ChassisNo,FuelType,Color,MeterReading")] Vehicle vehicle, List<IFormFile>? Images = null)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get the existing vehicle from the database
                var existingVehicle = await _context.Vehicle.FindAsync(id);
                if (existingVehicle == null)
                    return NotFound();

                // Update scalar properties
                existingVehicle.Make = vehicle.Make;
                existingVehicle.ModelNo = vehicle.ModelNo;
                existingVehicle.Classification = vehicle.Classification;
                existingVehicle.Origin = vehicle.Origin;
                existingVehicle.UsedCountry = vehicle.UsedCountry;
                existingVehicle.Year = vehicle.Year;
                existingVehicle.RegNo = vehicle.RegNo;
                existingVehicle.RegDate = vehicle.RegDate;
                existingVehicle.EngineNo = vehicle.EngineNo;
                existingVehicle.FuelSystem = vehicle.FuelSystem;
                existingVehicle.EngineCap = vehicle.EngineCap;
                existingVehicle.ChassisNo = vehicle.ChassisNo;
                existingVehicle.FuelType = vehicle.FuelType;
                existingVehicle.Color = vehicle.Color;
                existingVehicle.MeterReading = vehicle.MeterReading;
                existingVehicle.AppUserId = vehicle.AppUserId;


                try
                {
                    // Handle new image uploads
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    if (Images != null && Images.Count > 0)
                    {
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

                                // Add the new image path
                                existingVehicle.Images.Add("/uploads/" + fileName);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
