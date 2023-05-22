using ATH_UBB.Areas.Admin.Models;
using ATH_UBB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ATH_UBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        
        private readonly ApplicationDbContext _context;

        public HomeController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            var RoleList = roles.Select(r => new ApplicationRole()
            {
                Id = r.Id,
                Name = r.Name,
                
            });
            //            return View(VehicleList);

            return View(RoleList);
        }
        
    }
}
