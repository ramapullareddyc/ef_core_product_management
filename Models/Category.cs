using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    [Table("Categories", Schema = "public")]
    public class Category
    {
        [Key]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [StringLength(200)]
        [Column("Description")]
        public string Description { get; set; }

        [Column("ParentCategoryId")]
        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
} 