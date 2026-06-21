using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Клиент")]
    public class Client
    {
        [Key]
        [Column("Код_кл")]
        public int ClientId { get; set; }

        [Column("Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Column("Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Column("Отчество")]
        public string Patronymic { get; set; } = string.Empty;

        [Column("Номер")]
        public string Phone { get; set; } = string.Empty;

        [Column("Почта")]
        public string Email { get; set; } = string.Empty;

        [Column("Пароль")]
        public string Password { get; set; } = string.Empty;

        // НОВОЕ ПОЛЕ КОММЕНТАРИЯ
        [Column("Комментарий")]
        public string Comment { get; set; } = string.Empty;
    }
}