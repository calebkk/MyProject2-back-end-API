using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject2.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("Users")]
        public int? UserID { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey("Products")]
        public int? ProductID { get; set; }
        public virtual  Product? Product { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string Status { get; set; }
    }
}
