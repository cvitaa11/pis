using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantsReservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsReservations.Repository
{
    public class UserRepository : IUserRepository
    {
        #region fields
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region ctor
        public UserRepository(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        #region implementation
        public async Task<IList<IdentityUser>> GetAllAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            return admins;
        }
        #endregion
    }
}
