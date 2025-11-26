using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Products", Schema = "public")]
    public class Product
    {
        [Key]
        [Column("ProductId")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Column("Description")]
        public string Description { get; set; }

        [Column("Price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column("StockQuantity")]
        public int StockQuantity { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Column("SupplierId")]
        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        [StringLength(50)]
        [Column("SKU")]
        public string SKU { get; set; }

        [Column("Weight", TypeName = "decimal(18,2)")]
        public decimal? Weight { get; set; }

        [StringLength(50)]
        [Column("Dimensions")]
        public string Dimensions { get; set; }

        [Column("IsDiscontinued")]
        public bool IsDiscontinued { get; set; }

        [Column("ReorderLevel")]
        public int ReorderLevel { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        public ICollection<ProductHistory> History { get; set; }

        public override string ToString()
        {
            return $"Product ID: {ProductId}\n" +
                   $"Name: {Name}\n" +
                   $"Description: {Description}\n" +
                   $"Price: ${Price:N2}\n" +
                   $"Stock: {StockQuantity}\n" +
                   $"Created: {CreatedDate:g}\n" +
                   $"Modified: {(ModifiedDate.HasValue ? ModifiedDate.Value.ToString("g") : "Not modified")}";
        }
    }
} 