using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Categories_mod", Schema = "database-1_dbo")]
    public class Category
    {
        [Key]
        [Column("CategoryId_mod")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name_mod")]
        public string Name { get; set; }

        [StringLength(200)]
        [Column("Description_mod")]
        public string Description { get; set; }

        [Column("ParentCategoryId_mod")]
        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }

        [Column("CreatedDate_mod")]
        public DateTime CreatedDate { get; set; }
    }
} 