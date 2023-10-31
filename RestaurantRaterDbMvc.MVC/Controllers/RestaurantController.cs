using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantRaterDbMvc.Models.RestaurantModels;
using RestaurantRaterDbMvc.Services.RestaurantServices;

namespace RestaurantRaterDbMvc.MVC.Controllers;

[Route("[controller]")]
public class RestaurantController : Controller
{
    private readonly ILogger<RestaurantController> _logger;
    private readonly IRestaurantService _service;

    public RestaurantController(ILogger<RestaurantController> logger, IRestaurantService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        List<RestaurantListItem> restaurants = await _service.GetAllRestaurantsAsync();
        return View(restaurants);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantCreate model)
    {
        if(!ModelState.IsValid)
            return View(model);

        await _service.CreateRestaurantAsync(model);

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
