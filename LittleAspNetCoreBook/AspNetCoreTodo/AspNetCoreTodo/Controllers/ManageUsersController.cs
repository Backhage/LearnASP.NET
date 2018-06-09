using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Controllers
{   
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ManageUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            
            var admins = (await _userManager
                .GetUsersInRoleAsync(Constants.AdministratorRole))
                .ToArray();

            if (!admins.Contains(currentUser)) return Challenge();

            var everyone = await _userManager.Users.ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }
    }
}