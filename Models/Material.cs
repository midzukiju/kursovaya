using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Материал")]
    public class Material
    {
        [Key]
        [Column("Код_материал")]
        public int MaterialId { get; set; }

        [Column("Материал")]
        public string MaterialName { get; set; } = string.Empty;
    }
}