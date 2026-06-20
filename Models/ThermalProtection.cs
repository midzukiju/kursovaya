using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Теплозащитность")]
    public class ThermalProtection
    {
        [Key]
        [Column("Код_тепл")]
        public int ThermalId { get; set; }

        [Column("Тепл")]
        public string ThermalValue { get; set; } = string.Empty;
    }
}