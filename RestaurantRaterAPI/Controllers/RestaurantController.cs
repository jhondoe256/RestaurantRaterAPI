using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterAPI.Models;
using static RestaurantRaterAPI.RestaurantDbContext;
using System.Linq;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] //https:localhost/restaurant
    public class RestaurantController:Controller
    {
        private RestaurantDBContext _context;
        public RestaurantController(RestaurantDBContext context)
        {
            _context=context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromForm] RestaurantEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Restaurant.Add(new Restaurant
            {
                Name=model.Name,
                Location=model.Location
            });
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult>GetRestaurants()
        {
            var restaurants=await _context.Restaurant.Include(r=>r.Ratings).Select(r=>new RestaurantListItem
            {
                Id=r.Id,
                Name=r.Name,
                Location=r.Location,
                AverageScore= (float)r.AverageRating
            }).ToListAsync();
            return Ok(restaurants);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult>GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurant.Include(r=>r.Ratings).SingleOrDefaultAsync(r=>r.Id==id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>UpdateRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
        {
            var oldRestaurant= await _context.Restaurant.FindAsync(id);
            if (oldRestaurant is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                oldRestaurant.Name=model.Name;
            }
            if(!string.IsNullOrEmpty(model.Location))
            {
                oldRestaurant.Location=model.Location;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult>DeleteRestaurant([FromRoute]int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if(restaurant !=null)
            {
                _context.Restaurant.Remove(restaurant);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}