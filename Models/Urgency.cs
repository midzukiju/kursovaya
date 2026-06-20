using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Срочность")]
    public class Urgency
    {
        [Key]
        [Column("Код_срочн")]
        public int UrgencyId { get; set; }

        [Column("Срочность")]
        public string UrgencyName { get; set; } = string.Empty;
    }
}