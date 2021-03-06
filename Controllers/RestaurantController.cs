﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserRepository _userRepository;
        #endregion
        #region ctor
        public RestaurantController(IRestaurantRepository restaurantRepository, AppDbContext context, IUserRepository userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _context = context;
            _userRepository = userRepository;
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
        [HttpPost]
        public async Task<IActionResult> Reservation(int restId, string firstName, string lastName, DateTime resDate, string meal)
        {
            var reservation = new Reservation()
            {
                RestaurantId = restId,
                FirstName = firstName,
                LastName = lastName,
                Date = resDate,
                Meal = meal
            };

            TryValidateModel(reservation);

            if (ModelState.IsValid)
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AllReservations()
        {
            var reservations = _restaurantRepository.GetReservations();
            return View(reservations);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult MyReservations(string userId)
        {
            var myReservations = _restaurantRepository.GetMyReservations(userId);
            return View(myReservations);
        }

        public async Task<IActionResult> RemoveReservation(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(x => x.Id == id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("AllReservations");
            }
            return BadRequest();
        }
        #endregion

        #region getAdmins
        public async Task<IList<IdentityUser>> GetAllAdmins()
        {
            var admins = await _userRepository.GetAllAdmins();
            return admins;
        }
        #endregion

        #endregion
    }
}