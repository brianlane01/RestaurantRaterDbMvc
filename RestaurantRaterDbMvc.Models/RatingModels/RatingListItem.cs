using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterDbMvc.Models.RatingModels;

public class RatingListItem
{
    public int Id { get; set; }
    
    [Display(Name = "Restaurant")]
    public string RestaurantName { get; set; } = string.Empty;

    [Display(Name = " Food Rating")]
    public double FoodScore { get; set; }

    [Display(Name = " Cleanliness Rating")]
    public double CleanlinessScore { get; set; }

    [Display(Name = "Atmosphere Rating")]
    public double AtmospherScore { get; set; }

    [Display(Name = "Average Score")]
    public double Score { get; set; }
}
