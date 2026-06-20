using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Индивидуальный_заказ")]
    public class IndividualOrder
    {
        [Key]
        [Column("Код_ин")]
        public int IndOrderId { get; set; }

        [Column("Код_кл")]
        public int ClientId { get; set; }

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

        [Column("Дата_нач")]
        public DateTime StartDate { get; set; }

        [Column("Дата_пример")]
        public DateTime FitDate { get; set; }

        [Column("Дата_выд")]
        public DateTime IssueDate { get; set; }

        [Column("Код_срочн")]
        public int UrgencyId { get; set; }

        [Column("Стоимость")]
        public decimal Cost { get; set; }

        [Column("Аванс")]
        public decimal Advance { get; set; }
    }
}