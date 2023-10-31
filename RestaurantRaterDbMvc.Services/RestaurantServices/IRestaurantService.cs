using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterDbMvc.Models.RestaurantModels;

namespace RestaurantRaterDbMvc.Services.RestaurantServices;

public interface IRestaurantService
{
    Task<bool> CreateRestaurantAsync(RestaurantCreate model);
    Task<List<RestaurantListItem>> GetAllRestaurantsAsync();
    Task<RestaurantDetail> GetRestaurantByIdAsync(int id);
    Task<bool> UpdateRestaurantAsync(RestaurantUpdate model);
    Task<bool> DeleteRestaurantAsync(int id);
}
