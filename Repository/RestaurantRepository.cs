using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Reservation> GetReservations()
        {
            return _context.Reservations.Include("Restaurant").Where(x=> x.Date >= DateTime.Today).ToList();
        }

        public IEnumerable<Reservation> GetMyReservations(string userId)
        {
            return _context.Reservations.Include("Restaurant").Where(x => x.Date >= DateTime.Today && x.Restaurant.UserId == userId).ToList();
        }
    }
}
