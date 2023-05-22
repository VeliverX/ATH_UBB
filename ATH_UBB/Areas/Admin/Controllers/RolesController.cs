using ATH_UBB.Areas.Admin.Models;
using ATH_UBB.Data;
using ATH_UBB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ATH_UBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            var RoleList = roles.Select(r => new ApplicationRole()
            {
                Id = r.Id,
                Name = r.Name,
            });
            return View(RoleList);
        }
        public IActionResult Create()
        {
            return View(new ApplicationRole());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _roleManager.Roles == null)
            {
                return NotFound();
            }

            var role = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            var users = _userManager.Users.ToList();
            var usersList = users.Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,


            }
            );

            UserRolesViewModel viewModel = new UserRolesViewModel()
            {

                ApplicationRole = role,
                Users = new SelectList(users, "Id", "UserName")
            };

            return View(viewModel);
        }
    }
}

