using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterDbMvc.Data.Entities;

public class Restaurant
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

    public double AverageFoodScore
    {
        get
        {
            return Ratings.Count > 0 ? Ratings.Select(r => r.FoodScore).Sum() / Ratings.Count : 0;
        }
    }

    public double AverageCleanlinessScore
    {
        get
        {
            return Ratings.Count > 0 ? Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count : 0;
        }
    }

    public double AverageAtmosphereScore
    {
        get
        {
            return Ratings.Count > 0 ? Ratings.Select(r => r.AtmospherScore).Sum() / Ratings.Count : 0;
        }
    }

    public double Score
    {
        get
        {
            return (AverageAtmosphereScore + AverageCleanlinessScore + AverageFoodScore) / 3;
        }
    }
}
