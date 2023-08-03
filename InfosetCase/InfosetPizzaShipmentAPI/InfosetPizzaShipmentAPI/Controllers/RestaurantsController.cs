using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfosetShipmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private  IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("nearest")]
        public IActionResult GetNearestRestaurants( CustomerLocationDto location)
        {
            
            var result = _restaurantService.GetByClose(location);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        
        }
      


    }
}
