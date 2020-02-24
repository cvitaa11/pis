using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsReservations.Repository
{
    public interface IUserRepository
    {
        Task<IList<IdentityUser>> GetAllAdmins();
    }
}
