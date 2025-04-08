using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CadastroBasico.Models;
using CadastroBasico.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroBasico.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dashboardInfo = new Dictionary<string, int>
        {
            { "TotalUsers", await _context.Users.CountAsync() },
            { "ActiveUsers", await _context.Users.Where(u => u.Active).CountAsync() },
            { "TotalUserLevels", await _context.UserLevels.CountAsync() }
        };

        return View(dashboardInfo);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
