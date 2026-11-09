namespace ClothingStoreAPI.Models
{
    public class Clothing
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int PurchaseCount { get; set; } 
        public string Type { get; set; } = null!; 
        public bool IsAvailable { get; set; } = true; 

        public List<ClothingSize> Sizes { get; set; } = new();
    }
}
