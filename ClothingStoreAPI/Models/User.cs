namespace ClothingStoreAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User";

        public List<Order> Orders { get; set; } = new();
    }
}
