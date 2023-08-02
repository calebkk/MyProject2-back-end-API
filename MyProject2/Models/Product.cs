using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProject2.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public byte []? PictureData { get; set; }
        [JsonIgnore]
        public  string ?  PictureBase64String { get;  set; }

        [ForeignKey("Categories")]
        public int? CategoryID { get; set; }
        public virtual Category? Category { get; set; }

    }
}
