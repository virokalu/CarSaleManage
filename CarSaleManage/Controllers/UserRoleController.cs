using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Controllers;

public class UserRoleController : Controller
{
    private readonly  RoleManager<IdentityRole> _roleManager;

    public UserRoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    //List all the roles created by the User
    public IActionResult Index()
    {
        var roles = _roleManager.Roles;
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(IdentityRole identityRole)
    {
        //avoid duplicate role
        if (!_roleManager.RoleExistsAsync(identityRole.Name).GetAwaiter().GetResult()) 
        {
            _roleManager.CreateAsync(new IdentityRole(identityRole.Name)).GetAwaiter().GetResult();
        }
        return  RedirectToAction("Index");
    }
}
