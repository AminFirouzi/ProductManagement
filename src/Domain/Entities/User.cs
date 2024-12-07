using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User: IdentityUser
    {
        public string Email { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}