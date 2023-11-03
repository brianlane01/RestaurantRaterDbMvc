using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterDbMvc.Data;
using RestaurantRaterDbMvc.Data.Entities;
using RestaurantRaterDbMvc.Models.RatingModels;

namespace RestaurantRaterDbMvc.Services.RatingServices;

public class RatingService : IRatingService
{
    private readonly RestaurantDbContext _context;

    public RatingService(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateRatingAsync(RatingCreate model)
    {
        Rating rating = new Rating()
        {
            RestaurantId = model.RestaurantId,
            FoodScore = model.FoodScore,
            AtmospherScore = model.AtmospherScore,
            CleanlinessScore = model.CleanlinessScore
        };

        _context.Ratings.Add(rating);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<List<RatingListItem>> GetAllRatingsAsync()
    {
        List<RatingListItem> ratings = await _context.Ratings
            .Select(r => new RatingListItem()
            {
                Id = r.Id,
                RestaurantName = r.Restaurant.Name,
                FoodScore = r.FoodScore,
                CleanlinessScore = r.CleanlinessScore,
                AtmospherScore = r.AtmospherScore
            }).ToListAsync();

        return ratings; 
    }

    public async Task<List<RatingListItem>> GetAllRatingsByRestaurantIdAsync(int id)
    {
        List<RatingListItem> ratings = await _context.Ratings
            .Where(r => r.RestaurantId == id)
            .Select(r => new RatingListItem()
            {
                RestaurantName = r.Restaurant.Name,
                FoodScore = r.FoodScore,
                CleanlinessScore = r.CleanlinessScore,
                AtmospherScore = r.AtmospherScore,
                Score = r.Restaurant.Score
            }).ToListAsync();

        return ratings;
    }

    public async Task<RatingDetail?> GetRatingByIdAsync(int id)
    {
        Rating? rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.Id == id);

        return rating is null ? null : new RatingDetail()
        {
            Id = rating.Id, 
            RestaurantId = rating.RestaurantId,
            FoodScore = rating.FoodScore,
            CleanlinessScore = rating.CleanlinessScore,
            AtmospherScore = rating.AtmospherScore,
        };
    }


    public async Task<bool> DeleteRatingAsync(int id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        
        if (rating == null)
            return false;

        _context.Ratings.Remove(rating);
        return await _context.SaveChangesAsync() == 1;
    }
}
