using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Сотрудник")]
    public class Employee
    {
        [Key]
        [Column("Код_сотр")]
        public int EmployeeId { get; set; }

        [Column("Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Column("Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Column("Отчество")]
        public string Patronymic { get; set; } = string.Empty;

        [Column("Должность")]
        public string Position { get; set; } = string.Empty;

        [Column("Телефон")]
        public string Phone { get; set; } = string.Empty;

        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Column("Дата_приема")]
        public DateTime HireDate { get; set; }
    }
}