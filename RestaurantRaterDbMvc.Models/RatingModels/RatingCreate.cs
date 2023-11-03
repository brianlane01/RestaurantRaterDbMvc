using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantRaterDbMvc.Models.RatingModels;

public class RatingCreate
{
    [Required, Display(Name = "Restaurant")]
    public int RestaurantId { get; set; }

    [Required, Range(1, 10)]
    public double FoodScore { get; set; }

    [Required, Range(1, 10)]
    public double CleanlinessScore { get; set; }

    [Required, Range(1, 10)]
    public double AtmospherScore { get; set; }

    public IEnumerable<SelectListItem> RestaurantOptions {get; set;} = new List<SelectListItem>();
}
