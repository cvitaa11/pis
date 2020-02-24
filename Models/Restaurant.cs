using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsReservations.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        [StringLength(300, MinimumLength = 10)]
        [Required(ErrorMessage = "This field is required.")]
        public string Description { get; set; }
        [StringLength(15, MinimumLength = 3)]
        [Required(ErrorMessage = "This field is required.")]
        public string Specialities1 { get; set; }
        [StringLength(15, MinimumLength = 3)]
        [Required(ErrorMessage = "This field is required.")]
        public string Specialities2 { get; set; }
        [StringLength(15, MinimumLength = 3)]
        [Required(ErrorMessage = "This field is required.")]
        public string Specialities3 { get; set; }        
        [Required(ErrorMessage = "This field is required.")]
        public string CoverURL { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser IdentityUser { get; set; }
    }
}
