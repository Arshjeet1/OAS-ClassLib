using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;
using OAS_MVC.Models;

namespace OAS_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        TransactionServices TransactionService = new TransactionServices();
        List<Transaction> obj = TransactionService.GetAllTransactions();
        return View(obj);
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
