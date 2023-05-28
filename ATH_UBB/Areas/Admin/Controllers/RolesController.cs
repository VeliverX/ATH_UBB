using ATH_UBB.Areas.Admin.Models;
using ATH_UBB.Data;
using ATH_UBB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace ATH_UBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]

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
            var users = _userManager.Users.ToList();
            var userList = users.Select(r => new ApplicationUser()
            {
                Id = r.Id,
                UserName = r.UserName,
                Email = r.Email,
                
            });
            return View(userList);
        }
        public IActionResult Create()
        {
            return View(new ApplicationRole());
        }

        public async Task<IActionResult> SetAdmin(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            await _userManager.AddToRoleAsync(user, "Administrator");
            await _userManager.RemoveFromRoleAsync(user, "User");
            //await _context.SaveChangesAsync();

            return Redirect("/Admin");
        }
    }
}

