using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestaurantRaterDbMvc.Data;
using RestaurantRaterDbMvc.Data.Entities;
using RestaurantRaterDbMvc.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore;

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

    public Task<RestaurantDetail> GetRestaurantByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateRestaurantAsync(RestaurantUpdate model)
    {
        throw new NotImplementedException();
    }

}
