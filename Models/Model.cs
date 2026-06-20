using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Модель")]
    public class Model
    {
        [Key]
        [Column("Код_мод")]
        public int ModelId { get; set; }

        [Column("Модель")]
        public string ModelName { get; set; } = string.Empty;
    }
}