using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Suppliers", Schema = "dbo")]
    public class Supplier
    {
        [Key]
        [Column("SupplierId")]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Name")]
        public string Name { get; set; }

        [StringLength(200)]
        [Column("Address")]
        public string Address { get; set; }

        [StringLength(50)]
        [Column("ContactPerson")]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        [Column("Phone")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Column("Email")]
        public string Email { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }
    }
} 