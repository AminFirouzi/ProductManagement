namespace Domain.Entities
{
    public class User
    {
        // Primary Key: Email
        public string Email { get; set; } = string.Empty;

        // Navigation Property: One User can have many Products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}