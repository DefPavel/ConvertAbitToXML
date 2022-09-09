using System;

namespace ConvertAbitToXML.Models
{
    [Serializable]
    public class Reader
    {
        public int Code { get; set; } // Код читательского билета        
        public string Kod { get; set; } // ИНН студента        
        public string Name { get; set; } // ФИО      
        public string Adress { get; set; } // Адрес проживания
        public string Category { get; set;} // Категория персоны
        public string BirthDate { get; set; } // Дата рождения
        public string Photo { get; set; } // Фото человека
        public string PassportSeries { get; set; } // Серия паспорта
        public string PassportNo { get; set; } // Номер Паспорта
        public string PassportOrg { get; set; } // Кем выдан    
        public string WorkPlace { get; set; } // Инстиут-Факультет
        public string Post { get; set; } // По идеи (на какой специальности учиться)
        public int PagerPhone { get; set; } // Сюда рандомные 4 цифры    
        public int PagerNo { get; set; } // 0        
        public int IsBlocked { get; set; } // 0
        public string ServiceBegDate { get; set; } // 11.09.2021
        public int Comment { get; set; } // 0
        public string RegisterDate { get; set; } // 11.09.2021
    }
}
