using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    [Table("Рабочий_процесс")]
    public class WorkProcess
    {
        [Key]
        [Column("Код_процесса")]
        public int ProcessId { get; set; }

        [Column("Код_сотр")]
        public int EmployeeId { get; set; }

        [Column("Код_зак")]
        public int OrderId { get; set; }

        [Column("Этап_работы")]
        public string WorkStage { get; set; } = string.Empty;

        [Column("Дата_начала")]
        public DateTime StartDate { get; set; }

        [Column("Дата_окончания")]
        public DateTime EndDate { get; set; }
    }
}