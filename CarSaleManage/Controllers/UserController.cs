using CarSaleManage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSaleManage.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<AppUser> userManager;
        private readonly IUserStore<AppUser> userStore;

        public UserController(UserManager<AppUser> userManager, IUserStore<AppUser> userStore) 
        {
            this.userManager = userManager;
            this.userStore = userStore;
        }
        //GET: User
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();

            return View(users);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,PhoneNumber,Firstname,Lastname")] AppUser user)
        {
            if (ModelState.IsValid)
            {
                await userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(user);
        }

        // GET: User/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(id.ToString()!);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,PhoneNumber,FirstName,LastName")] AppUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
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

            var user = await userManager.FindByIdAsync(id.ToString()!);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id.ToString()!);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id.ToString()!);
            if (user == null)
            {
                return NotFound();
            }

            await userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
