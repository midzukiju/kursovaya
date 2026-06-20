using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Поставщик")]
    public class Supplier
    {
        [Key]
        [Column("Код_пост")]
        public int SupplierId { get; set; }

        [Column("Название")]
        public string Name { get; set; } = string.Empty;

        [Column("Контактное_лицо")]
        public string ContactPerson { get; set; } = string.Empty;

        [Column("Телефон")]
        public string Phone { get; set; } = string.Empty;

        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Column("Адрес")]
        public string Address { get; set; } = string.Empty;
    }
}