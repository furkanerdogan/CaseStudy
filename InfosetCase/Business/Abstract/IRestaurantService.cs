using Core.Utilities.Results;
using Entities;
using Entities.Concrete;
using Entities.Dtos;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRestaurantService
    {
        IResult Add(Restaurant restaurant);
        IResult Delete(Restaurant restaurant);
        IResult Update(Restaurant restaurant);
        IDataResult<List<Restaurant>> GetAll();
        IDataResult<Restaurant> GetById(int restaurantId);
        IDataResult<List<Restaurant>> GetByClose(CustomerLocationDto location);

    }
}
