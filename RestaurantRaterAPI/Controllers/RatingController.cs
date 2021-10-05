using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterAPI.Models;
using Microsoft.EntityFrameworkCore;
using static RestaurantRaterAPI.RestaurantDbContext;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController:Controller
    {
        private RestaurantDBContext _context;
        public RatingController(RestaurantDBContext context)
        {
            _context=context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromForm] RatingEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Rating.Add(new Rating
            {
                Score=model.Score,
                RestaurantId=model.RestaurantId
            });
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult>GetRatings()
        {
            var ratings=await _context.Rating.ToListAsync();
            return Ok(ratings);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRating([FromRoute] int id)
        {
            var rating=await _context.Rating.FindAsync(id);
            if(rating is null)
            {
                return NotFound();
            }
            return Ok(rating);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRating([FromForm] RatingEdit model, [FromRoute] int id)
        {
            var oldModel = await _context.Rating.FindAsync(id);
            if(oldModel is null)
            {
                return NotFound();
            }
            if(ModelState.IsValid ==false)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            var oldModel=await _context.Rating.FindAsync(id);
            if (oldModel is null)
            {
                return BadRequest();
            }
            if(id<1)
            {
                return BadRequest();
            }
            _context.Rating.Remove(oldModel);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}