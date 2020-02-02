using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantsReservations.Models;
using RestaurantsReservations.Repository;

namespace RestaurantsReservations.Controllers
{
    public class RestaurantController : Controller
    {
        #region fields
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly AppDbContext _context;
        #endregion
        #region ctor
        public RestaurantController(IRestaurantRepository restaurantRepository, AppDbContext context)
        {
            _restaurantRepository = restaurantRepository;
            _context = context;
        }
        #endregion
        #region implementation
        public IActionResult Index()
        {
            var restaurants = _restaurantRepository.GetAllRestaurants();
            
            return View(restaurants); 
        }
        #region create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        #endregion

        #region update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Restaurant restaurant)
        {            
            if (ModelState.IsValid)
            {
                _context.Update(restaurant);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        #endregion

        #region delete
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = _context.Restaurants.SingleOrDefault(x => x.Id == id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        #endregion

        #region details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);           
        }
        #endregion

        #region reservation
            [Authorize]
            public IActionResult Reservation()
            {
                return View();
            }
            #endregion

        #endregion
    }
}