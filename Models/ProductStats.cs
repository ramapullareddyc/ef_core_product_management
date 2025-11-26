using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("ProductStats", Schema = "public")]
    public class ProductStats
    {
        [Key]
        [Column("StatId")]
        public int StatId { get; set; }

        [Column("TotalProducts")]
        public int TotalProducts { get; set; }
        
        [Column("AveragePrice", TypeName = "decimal(18,2)")]
        public decimal AveragePrice { get; set; }
        
        [Column("TotalStockValue", TypeName = "decimal(18,2)")]
        public decimal TotalStockValue { get; set; }
        
        [Column("LowStockCount")]
        public int LowStockCount { get; set; }
        
        [Column("DiscontinuedCount")]
        public int DiscontinuedCount { get; set; }
        
        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }
    }
} 