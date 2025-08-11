using CarSaleManage.Models.Dtos;
using CarSaleManage.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSaleManage.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            this._userService = userService;
        }
        //GET: User
        public async Task<IActionResult> Index()
        {
            string? searchString = Request.Query["searchString"];
            return View(await _userService.SearchListAsync(searchString));
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(user);
                if(result.Succeeded) return RedirectToAction(nameof(Index));

                return View(user);
            }
            return View(user);
        }

        // GET: User/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _userService.GetAsync(id);
            if(result.Success)
            {
                var user = result.Data;
                if(user != null)
                {
                    var userEditDto = new UserEditDto
                    {
                        Id = user.Id,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        PhoneNumber = user.PhoneNumber,
                        EmailConfirmed = user.EmailConfirmed,
                    };
                    return View(userEditDto);
                }

            }
            return NotFound();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditDto user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateAsync(user);
                if (result.Succeeded) return RedirectToAction(nameof(Index));

                NotFound();
                Console.WriteLine(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            return View(user);
        }

        // Get: User/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _userService.GetAsync(id);
            if(result.Success) return View(result.Data);

            Console.WriteLine(result.Error);
            return NotFound();
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _userService.GetAsync(id);
            if (result.Success)
                return View(result.Data);

            Console.WriteLine(result.Error);
            return NotFound();
        }


        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if(result.Succeeded) return RedirectToAction(nameof(Index));

            Console.WriteLine(string.Join("; ", result.Errors.Select(e => e.Description)));
            return NotFound();
        }
    }
}
