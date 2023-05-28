using ATH_UBB.Areas.Admin.Models;
using ATH_UBB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATH_UBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly ApplicationDbContext _context;

        public HomeController(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;


        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            var roleList = new List<ApplicationRole>();

            foreach (var role in roles)
            {
                var usersInRole = _context.UserRoles.Count(ur => ur.RoleId == role.Id);
                var roleViewModel = new ApplicationRole
                {
                    Id = role.Id,
                    Name = role.Name,
                    userCount = usersInRole
                };

                roleList.Add(roleViewModel);
            }

            return View(roleList);
        }


    }
}
