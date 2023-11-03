using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        if(!ModelState.IsValid)
            return View();

        var restaurant = await _service.GetRestaurantByIdAsync(id);

        if (restaurant == null)
            return RedirectToAction(nameof(Index));

        var restaurantEdit = new RestaurantUpdate()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Location = restaurant.Location
        };
        
        return View(restaurantEdit);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RestaurantUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(ModelState);
        }

        var restaurant = await _service.UpdateRestaurantAsync(model);

        if (!restaurant)
            return RedirectToAction(nameof(Index));

        return RedirectToAction("Details", new { id = model.Id });
        // return RedirectToAction("Details");

    }

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantCreate model)
    {
        if(!ModelState.IsValid)
            return View(model);
        

        await _service.CreateRestaurantAsync(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        if(!ModelState.IsValid)
            return View();

        var restaurant = await _service.GetRestaurantByIdAsync(id);

        if (restaurant == null)
            return RedirectToAction(nameof(Index));

        return View(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(RestaurantDetail model)
    {
        if (!ModelState.IsValid)
        {
            return View(ModelState);
        }

        await _service.DeleteRestaurantAsync(model.Id);

        return RedirectToAction(nameof(Index));

    }
}
