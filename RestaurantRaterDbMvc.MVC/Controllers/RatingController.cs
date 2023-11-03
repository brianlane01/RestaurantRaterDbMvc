using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterDbMvc.Data;
using RestaurantRaterDbMvc.Models.RatingModels;
using RestaurantRaterDbMvc.Services.RatingServices;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterDbMvc.Services.RestaurantServices;
using RestaurantRaterDbMvc.Models.RestaurantModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantRaterDbMvc.MVC.Controllers;

public class RatingController : Controller
{
    private readonly IRatingService _service;
    private readonly IRestaurantService _restaurantService;

    public RatingController(IRatingService service,
            IRestaurantService restaurantService)
    {
        _service = service;
        _restaurantService = restaurantService;
    }

    public async Task<IActionResult> Index()
    {
        List<RatingListItem> ratings = await _service.GetAllRatingsAsync();
        return View(ratings);
    }

    public async Task<IActionResult> Create()
    {
        List<RestaurantDetail> restaurants = await _restaurantService.GetAllRestaurantsDetailAsync();
        
        IEnumerable<SelectListItem> restaurantOptions = restaurants
            .Select(r => new SelectListItem()
            {
                Text = $"{r.Name} - {r.Location}",
                Value = r.Id.ToString()
            }).ToList();

        RatingCreate model = new RatingCreate();
        model.RestaurantOptions = restaurantOptions;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RatingCreate model)
    {
        if(!ModelState.IsValid)
            return View(model);
        

        await _service.CreateRatingAsync(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Restaurant(int id)
    {
        if(!ModelState.IsValid)
            return View();

        var ratings = await _service.GetAllRatingsByRestaurantIdAsync(id);
        
        if (ratings == null)
            return RedirectToAction(nameof(Index));

        RestaurantDetail restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        ViewBag.RestaurantName = restaurant.Name;

        return View(ratings);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if(!ModelState.IsValid)
            return View();

        var rating = await _service.GetRatingByIdAsync(id);

        if (rating == null)
            return RedirectToAction(nameof(Index));

        return View(rating);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(RatingDetail model)
    {
        if (!ModelState.IsValid)
        {
            return View(ModelState);
        }

        await _service.DeleteRatingAsync(model.Id);

        return RedirectToAction(nameof(Index));

    }
}
