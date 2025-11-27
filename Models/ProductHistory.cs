using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("ProductHistory_mod", Schema = "database-1_dbo")]
    public class ProductHistory
    {
        [Key]
        [Column("HistoryId_mod")]
        public int HistoryId { get; set; }

        [Column("ProductId_mod")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [StringLength(10)]
        [Column("Action_mod")]
        public string Action { get; set; }

        [Column("OldPrice_mod", TypeName = "decimal(18,2)")]
        public decimal? OldPrice { get; set; }

        [Column("NewPrice_mod", TypeName = "decimal(18,2)")]
        public decimal? NewPrice { get; set; }

        [Column("OldStock_mod")]
        public int? OldStock { get; set; }
        [Column("NewStock_mod")]
        public int? NewStock { get; set; }

        [Column("ActionDate_mod")]
        public DateTime ActionDate { get; set; }

        [StringLength(100)]
        [Column("ModifiedBy_mod")]
        public string ModifiedBy { get; set; }
    }
} 