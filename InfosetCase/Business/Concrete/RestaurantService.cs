using Business.Abstract;
using Core.Utilities.Results;
using Entities;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Business.Constants;
using Entities.Concrete;
using GeoCoordinatePortable;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos;

namespace Business.Concrete
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantDal _restaurantDal;

        public RestaurantService(IRestaurantDal restaurantDal)
        {
            _restaurantDal = restaurantDal;
        }

        public IResult Add(Restaurant restaurant)
        {

            IResult result = CheckIfRestaurantNameExists(restaurant.Name);

            if (result != null)
            {
                return new SuccessResult(Messages.RestaurantAlredyExist);
            }
            _restaurantDal.Add(restaurant);
            return new SuccessResult(Messages.RestaurantAdded);
        }
        public IResult Update(Restaurant restaurant)
        {

            _restaurantDal.Update(restaurant);
            return new SuccessResult(Messages.RestaurantUpdated);
        }
        public IResult Delete(Restaurant restaurant)
        {
            _restaurantDal.Delete(restaurant);
            return new SuccessResult(Messages.RestaurantDeleted);
        }

        private IResult CheckIfRestaurantNameExists(string name)
        {

            var result = _restaurantDal.GetList(p => p.Name == name).Any();
            if (result)
            {
                return new ErrorResult(Messages.RestaurantAlredyExist);
            }

            return null;
        }
      
        public IDataResult<Restaurant> GetById(int restaurantId)
        {
            return new SuccessDataResult<Restaurant>(_restaurantDal.Get(p => p.Id == restaurantId));
        }

        public IDataResult<List<Restaurant>> GetAll()
        {
            return new SuccessDataResult<List<Restaurant>>(_restaurantDal.GetList().ToList());
        }

        public IDataResult<List<Restaurant>> GetByClose(CustomerLocationDto location)
        {
            var nearbyRestaurants = _restaurantDal.GetList().Select(restaurant => new
            {
                Restaurant = restaurant,
                Distance = CalculateDistance(location.Latitude, location.Longitude, restaurant.Latitude, restaurant.Longitude)
            })
        .Where(x => x.Distance <= 10) // 10km 
        .OrderBy(x => x.Distance) // Order by distance
        .Take(5) // Take the nearest 5
        .Select(x => x.Restaurant)
        .ToList();
            return new SuccessDataResult<List<Restaurant>>(nearbyRestaurants);

        }
        private double CalculateDistance(double customerLatitude, double customerLongitude, double restaurantLatitude, double restaurantLongitude)
        {
            return 6371 * Math.Acos(
                Math.Cos(DegreesToRadians(customerLatitude)) * Math.Cos(DegreesToRadians(restaurantLatitude)) * Math.Cos(DegreesToRadians(restaurantLongitude) - DegreesToRadians(customerLongitude)) +
                Math.Sin(DegreesToRadians(customerLatitude)) * Math.Sin(DegreesToRadians(restaurantLatitude))
            );
        }
        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
