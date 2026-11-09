namespace ClothingStoreAPI.DTOs;

public class RegisterDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
}