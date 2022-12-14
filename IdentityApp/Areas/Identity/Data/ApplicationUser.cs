using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public DateTime registrationDate { get; set; } = DateTime.Now;
    public DateTime lastLoginDate { get; set; } = DateTime.Now;
    public bool statusBlock { get; set; }
    
}
