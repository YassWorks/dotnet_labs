using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP2.Models;
using TP2.Data;

namespace TP2.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users;
        return View(users);
    }

    public IActionResult UserCart()
    {
        var currentUser = _userManager.GetUserId(User);
        var carts = _context.UserCarts
            .Where(c => c.UserId == currentUser)
            .ToList();
        return View(carts);
    }
}
