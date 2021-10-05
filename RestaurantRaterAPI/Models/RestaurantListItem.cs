namespace RestaurantRaterAPI.Models
{
    public class RestaurantListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public float AverageScore { get; set; }
    }
}