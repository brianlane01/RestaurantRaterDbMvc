using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterDbMvc.Data.Entities;

public class Rating
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public double FoodScore { get; set; }
    public double CleanlinessScore { get; set; }
    public double AtmospherScore { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
