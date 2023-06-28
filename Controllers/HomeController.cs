using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // ===================================================================
    // Route - Index Page Where All Chefs Are Displayed
    // ===================================================================
    public IActionResult Index()
    {
        MyViewModel MyModel = new MyViewModel
        {
            AllChefs = _context.Chefs.Include(c => c.AllDishes).ToList()
        };

        return View(MyModel);
    }


    // ===================================================================
    // Route - Chef Creation Page
    // ===================================================================
    [HttpGet("chefs/create")]
    public IActionResult AddChef()
    {
        return View();
    }


    // ===================================================================
    // Route - Create A New Chef
    // ===================================================================
    [HttpPost("chefs/new")]
    public IActionResult NewChef(Chef newChef)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } else {
            return View("AddChef");
        }
    }


    // ===================================================================
    // Route - All Dishes Are Displayed
    // ===================================================================
    [HttpGet("dishes")]
    public IActionResult Dishes()
    {
        MyViewModel MyModel = new MyViewModel
        {
            AllDishes = _context.Dishes.ToList()
        };

        ViewBag.AllChefs = _context.Chefs.ToList();

        return View(MyModel);
    }


    // ===================================================================
    // Route - Dish Creation Page
    // ===================================================================
    [HttpGet("dishes/create")]
    public IActionResult AddDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();

        return View("AddDish");
    }


    // ===================================================================
    // Route - Create A New Dish
    // ===================================================================
    [HttpPost("dishes/new")]
    public IActionResult NewDish(Dish newDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Dishes");
        } else {
            return AddDish();
        }
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
