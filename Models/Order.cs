using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Заказ")]
    public class Order
    {
        [Key]
        [Column("Код_зак")]
        public int OrderId { get; set; }

        [Column("Код_кл")]
        public int ClientId { get; set; }

        [Column("Код_ка")]
        public int CatalogId { get; set; }

        [Column("дата_нач")]
        public DateTime StartDate { get; set; }

        [Column("дата_выд")]
        public DateTime IssueDate { get; set; }

        [Column("Код_срочн")]
        public int UrgencyId { get; set; }

        [Column("стоимость")]
        public decimal Cost { get; set; }

        [Column("аванс")]
        public decimal Advance { get; set; }
    }
}