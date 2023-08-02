using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject2.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        

        [ForeignKey("Users")]

        public int UserID { get; set; } // Foreign key to User table
        public virtual User? User { get; set; }


        [ForeignKey("Products")]
        public int ProductID { get; set; } // Foreign key to Product table
        
        public virtual Product? Product { get; set; }
    }
}
