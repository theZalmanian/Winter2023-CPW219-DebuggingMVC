using System.ComponentModel.DataAnnotations;

namespace CPW219_AspnetMVC_CRUD_Debugging.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(35)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; } = null!; // Added "null!" to ignore compiler warning
    }
}
