namespace project.Entities
{
    public class BasketItemDto
    {
        public int BasketId { get; set; }
        public string ProductName { get; set; }
        public decimal price { get; set; }

        public int Quantity { get; set; }

        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}