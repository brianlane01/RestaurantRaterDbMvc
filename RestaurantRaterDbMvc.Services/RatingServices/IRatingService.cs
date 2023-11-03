using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterDbMvc.Models.RatingModels;

namespace RestaurantRaterDbMvc.Services.RatingServices;

public interface IRatingService
{
    Task<bool> CreateRatingAsync(RatingCreate model);
    Task<List<RatingListItem>> GetAllRatingsAsync();
    Task<List<RatingListItem>> GetAllRatingsByRestaurantIdAsync(int id);
    Task<bool> DeleteRatingAsync(int id);
    Task<RatingDetail> GetRatingByIdAsync(int id);
    
}
