using System;

namespace kurs.ViewModels
{
    public class OrderViewModel
    {
        public int Код { get; set; }
        public int Код_клиента { get; set; }
        public int Код_каталога { get; set; }
        public DateTime Дата_начала { get; set; }
        public DateTime Дата_выдачи { get; set; }
        public int Код_срочности { get; set; }
        public decimal Стоимость { get; set; }
        public decimal Аванс { get; set; }
    }
}