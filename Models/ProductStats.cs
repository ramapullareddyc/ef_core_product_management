using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("ProductStats_mod", Schema = "database-1_dbo")]
    public class ProductStats
    {
        [Key]
        [Column("StatId_mod")]
        public int StatId { get; set; }

        [Column("TotalProducts_mod")]
        public int TotalProducts { get; set; }
        [Column("AveragePrice_mod", TypeName = "decimal(18,2)")]
        public decimal AveragePrice { get; set; }
        [Column("TotalStockValue_mod", TypeName = "decimal(18,2)")]
        public decimal TotalStockValue { get; set; }
        [Column("LowStockCount_mod")]
        public int LowStockCount { get; set; }
        [Column("DiscontinuedCount_mod")]
        public int DiscontinuedCount { get; set; }
        [Column("LastUpdated_mod")]
        public DateTime LastUpdated { get; set; }
    }
} 