﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContainersPortal.Models;
using Microsoft.EntityFrameworkCore;
using ContainersPortal.Services;
using ContainersPortal.Database;

namespace ContainersPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserContext _context;
    private readonly LinuxHelperService _linuxHelperService;

    public HomeController(ILogger<HomeController> logger, UserContext context, LinuxHelperService linuxHelperService)
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Students()
    {
        var students = await _context.Users
            .Where(u => u.UserName.ToLower() != "admin")
            .ToListAsync();

        StudentCollection studentCollection = new StudentCollection();
        studentCollection.Students = students;

        return View(studentCollection);
    }
}
