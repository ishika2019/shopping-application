using project.Entities;

namespace project.DTO
{
    public class ProductDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public String producttype { get; set; }
        
        public String productbrand { get; set; }
       
    }
}
