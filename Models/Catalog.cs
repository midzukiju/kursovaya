using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Каталог")]
    public class Catalog
    {
        [Key]
        [Column("Код_ка")]
        public int CatalogId { get; set; }

        [Column("Код_мод")]
        public int ModelId { get; set; }

        [Column("Код_материал")]
        public int MaterialId { get; set; }

        [Column("Код_цвет")]
        public int ColorId { get; set; }

        [Column("Размер")]
        public string Size { get; set; } = string.Empty;

        [Column("Код_тепл")]
        public int ThermalId { get; set; }
    }
}