namespace ClothingStoreAPI.Models
{
    public class ClothingSize
    {
        public int Id { get; set; }
        public string LetterSize { get; set; } = null!; 
        public int NumericSize { get; set; } 
        public int StockQuantity { get; set; } 

        public int ClothingId { get; set; }
        public Clothing Clothing { get; set; } = null!;
    }
}
