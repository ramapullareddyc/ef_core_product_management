using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Products_mod", Schema = "database-1_dbo")]
    public class Product
    {
        [Key]
        [Column("ProductId_mod")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Name_mod")]
        public string Name { get; set; }

        [StringLength(500)]
        [Column("Description_mod")]
        public string Description { get; set; }

        [Column("Price_mod", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column("StockQuantity_mod")]
        public int StockQuantity { get; set; }

        [Column("CategoryId_mod")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Column("SupplierId_mod")]
        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        [StringLength(50)]
        [Column("SKU_mod")]
        public string SKU { get; set; }

        [Column("Weight_mod", TypeName = "decimal(18,2)")]
        public decimal? Weight { get; set; }

        [StringLength(50)]
        [Column("Dimensions_mod")]
        public string Dimensions { get; set; }

        [Column("IsDiscontinued_mod")]
        public bool IsDiscontinued { get; set; }

        [Column("ReorderLevel_mod")]
        public int ReorderLevel { get; set; }

        [Column("CreatedDate_mod")]
        public DateTime CreatedDate { get; set; }

        [Column("ModifiedDate_mod")]
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