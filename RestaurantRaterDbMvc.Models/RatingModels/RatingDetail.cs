using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterDbMvc.Models.RatingModels;

public class RatingDetail
{
    public int Id { get; set; }

    [Display(Name = "Restaurant")]
    public int RestaurantId { get; set; }

    [Display(Name = " Food Rating")]
    public double FoodScore { get; set; }

    [Display(Name = " Cleanliness Rating")]
    public double CleanlinessScore { get; set; }

    [Display(Name = "Atmosphere Rating")]
    public double AtmospherScore { get; set; }
}