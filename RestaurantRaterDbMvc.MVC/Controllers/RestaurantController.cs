using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.Extensions.Logging;
using RestaurantRaterDbMvc.Models.RestaurantModels;
using RestaurantRaterDbMvc.Services.RestaurantServices;

namespace RestaurantRaterDbMvc.MVC.Controllers;

// [Route("[controller]")]
public class RestaurantController : Controller
{
    private readonly IRestaurantService _service;

    public RestaurantController(IRestaurantService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        List<RestaurantListItem> restaurants = await _service.GetAllRestaurantsAsync();
        return View(restaurants);
    }

    [ActionName("Details")]
    public async Task<IActionResult> Restaurant(int id)
    {
        if(!ModelState.IsValid)
            return View();

        var restaurant = await _service.GetRestaurantByIdAsync(id);
        
        if (restaurant == null)
            return RedirectToAction(nameof(Index));

        return View(restaurant);
    

    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantCreate model)
    {
        if(!ModelState.IsValid)
            return View(model);
        

        await _service.CreateRestaurantAsync(model);

        return RedirectToAction(nameof(Index));
    }
}
