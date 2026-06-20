using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Цвет")]
    public class Color
    {
        [Key]
        [Column("Код_цвет")]
        public int ColorId { get; set; }

        [Column("Цвет")]
        public string ColorName { get; set; } = string.Empty;
    }
}