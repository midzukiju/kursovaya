using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Поставка")]
    public class Supply
    {
        [Key]
        [Column("Код_поставки")]
        public int SupplyId { get; set; }

        [Column("Код_пост")]
        public int SupplierId { get; set; }

        [Column("Код_материал")]
        public int MaterialId { get; set; }

        [Column("Дата_поставки")]
        public DateTime SupplyDate { get; set; }

        [Column("Количество")]
        public int Quantity { get; set; }

        [Column("Цена_за_единицу")]
        public decimal UnitPrice { get; set; }
    }
}