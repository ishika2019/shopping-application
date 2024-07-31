using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Entities
{
    public class ProductBrand :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }
    }
}
