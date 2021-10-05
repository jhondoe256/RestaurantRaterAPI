using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantId {get;set;}
        [Required]
        public double Score{get;set;}
    }
}