using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Controllers;

public class RolesController : Controller
{
    private readonly  RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    //List all the roles created by the User
    public IActionResult Index()
    {
        var roles = _roleManager.Roles;
        return View(roles);
    }

    //[HttpPost]
    //public async Task<IActionResult> Create(string identityRole)
    //{
    //    //avoid duplicate role
    //    if (identityRole != null) 
    //    {
    //        await _roleManager.CreateAsync(new IdentityRole(identityRole.Trim()));
    //    }
    //    return  RedirectToAction("Index");
    //}
}
