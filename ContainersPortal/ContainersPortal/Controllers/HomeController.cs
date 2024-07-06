using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContainersPortal.Models;
using Microsoft.EntityFrameworkCore;
using ContainersPortal.Services;

namespace ContainersPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _context;
    private readonly LinuxHelperService _linuxHelperService;
    
    public HomeController(ILogger<HomeController> logger, DatabaseContext context, LinuxHelperService linuxHelperService)
    {
        _logger = logger;
        _context = context;
        _linuxHelperService = linuxHelperService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetResult()
    {
        // Your logic here to calculate or retrieve the result
        var ipAddress = (_linuxHelperService.ExecuteCommandOnHost("milos", "192.168.1.105", "laza", LinuxHelperService.GET_HOST_IP_ADDRESS)).ToString();
        ViewData["Result"] = ipAddress;

        return View("Index");
        //return PartialView("_ResultPartial", result);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Students()
    {
        var students = await _context.Students.ToListAsync();

        return View(students); 
    }
}
