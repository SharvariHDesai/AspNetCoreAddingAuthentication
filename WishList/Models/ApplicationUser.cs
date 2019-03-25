using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace WishList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Item> Item { get; set; }
    }
}
