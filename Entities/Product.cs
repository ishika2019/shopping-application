using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Entities
{
    public class Product : BaseEntity
    {
       
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl {  get; set; }

        public ProductType producttype { get; set; }
        public int ProductTypeId { get; set; }

        public ProductBrand productbrand { get; set; }
        public int ProductBrandId { get; set; }

    }
}
