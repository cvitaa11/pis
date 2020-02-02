using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantsReservations.Models;

namespace RestaurantsReservations.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        #region fields
        private readonly AppDbContext _context;
        #endregion

        #region ctor
        public RestaurantRepository(AppDbContext context)
        {
            _context = context;
        }
	    #endregion
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
           return _context.Restaurants.ToList();          
        }
    }
}
