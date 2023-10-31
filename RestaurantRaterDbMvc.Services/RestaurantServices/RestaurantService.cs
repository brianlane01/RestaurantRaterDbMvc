using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestaurantRaterDbMvc.Data;
using RestaurantRaterDbMvc.Data.Entities;
using RestaurantRaterDbMvc.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantRaterDbMvc.Services.RestaurantServices;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _context;
    public RestaurantService(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateRestaurantAsync(RestaurantCreate model)
    {
        Restaurant restaurant = new Restaurant()
        {
            Name = model.Name,
            Location = model.Location,
        };

        _context.Restaurants.Add(restaurant);
        return await _context.SaveChangesAsync() == 1;
    }

    public Task<bool> DeleteRestaurantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RestaurantListItem>> GetAllRestaurantsAsync()
    {
        List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score,
            }).ToListAsync();

        return restaurants;
    }

    public async Task<RestaurantDetail?> GetRestaurantByIdAsync(int id)
    {
        Restaurant? restaurant = await _context.Restaurants
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync(r => r.Id == id);

        return restaurant is null ? null : new RestaurantDetail()
        {
            Id = restaurant.Id, 
            Name = restaurant.Name,
            Location = restaurant.Location,
            Score = restaurant.Score
        };
    }

    public Task<bool> UpdateRestaurantAsync(RestaurantUpdate model)
    {
        throw new NotImplementedException();
    }

}
