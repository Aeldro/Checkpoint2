using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkpoint2.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Checkpoint2.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public List<CartArticle> Cart { get; set; } = new List<CartArticle>();
}

