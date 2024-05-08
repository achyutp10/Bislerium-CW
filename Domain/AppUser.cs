using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppUser : IdentityUser
    {

        public string? FirstName { get; set; }


        public string? LastName { get; set; }

        public int? Age { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
