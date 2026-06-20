using System;

namespace kurs.ViewModels
{
    public class EmployeeViewModel
    {
        public int Код { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Должность { get; set; }
        public string Телефон { get; set; }
        public string Email { get; set; }
        public DateTime Дата_приема { get; set; }
    }
}