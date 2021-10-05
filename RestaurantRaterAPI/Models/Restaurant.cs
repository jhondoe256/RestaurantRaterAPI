

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name {get;set;}
        [Required]
        [MaxLength(100)]
        public string Location{get;set;}
        public List<Rating> Ratings { get; set; } =new List<Rating>();

        public double AverageRating
        {
            get
            {
                if (Ratings.Count==0)
                {
                    return 0;
                }
                // double total =0;
                return Ratings.Average(r=>r.Score);
            }
        }
    }
}