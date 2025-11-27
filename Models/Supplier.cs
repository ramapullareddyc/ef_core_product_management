using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Suppliers_mod", Schema = "database-1_dbo")]
    public class Supplier
    {
        [Key]
        [Column("SupplierId_mod")]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Name_mod")]
        public string Name { get; set; }

        [StringLength(200)]
        [Column("Address_mod")]
        public string Address { get; set; }

        [StringLength(50)]
        [Column("ContactPerson_mod")]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        [Column("Phone_mod")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Column("Email_mod")]
        public string Email { get; set; }

        [Column("IsActive_mod")]
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }
    }
} 