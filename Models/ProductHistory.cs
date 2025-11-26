using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("ProductHistory", Schema = "dbo")]
    public class ProductHistory
    {
        [Key]
        [Column("HistoryId")]
        public int HistoryId { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [StringLength(10)]
        [Column("Action")]
        public string Action { get; set; }

        [Column("OldPrice", TypeName = "decimal(18,2)")]
        public decimal? OldPrice { get; set; }

        [Column("NewPrice", TypeName = "decimal(18,2)")]
        public decimal? NewPrice { get; set; }

        [Column("OldStock")]
        public int? OldStock { get; set; }
        
        [Column("NewStock")]
        public int? NewStock { get; set; }

        [Column("ActionDate")]
        public DateTime ActionDate { get; set; }

        [StringLength(100)]
        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }
    }
} 