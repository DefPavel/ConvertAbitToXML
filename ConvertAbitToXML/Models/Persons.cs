using System;
using System.Text.Json.Serialization;

namespace ConvertAbitToXML
{
    // Модель для JSON -> Abt.lgpu.org
    public class Persons
    {
        [JsonPropertyName("id")]
        public int Code { get; set; } // Код читательского билета
        [JsonPropertyName("indkod")]
        public string Kod { get; set; } // ИНН студента

        [JsonPropertyName("famil")]
        public string FistName { get; set; } // Ф

        [JsonPropertyName("name")]
        public string Name { get; set; } // И
        [JsonPropertyName("otch")]
        public string LastName { get; set; } // О

        public string FullName => $"{FistName} {Name} {LastName}";

        [JsonPropertyName("fact_residence")]
        public string Adress { get; set; } // Адрес проживания

        private string _Category;
        [JsonPropertyName("fo_name")]
        public string Category
        {

            get => _Category;
            set => _Category = value;
        } // Категория персоны

        [JsonPropertyName("birthday")]
        public DateTime BirthDate { get; set; } // Дата рождения

        [JsonPropertyName("photo_url")]
        public string Photo { get; set; } // Фото человека

        [JsonPropertyName("pasp_ser")]
        public string PassportSeries { get; set; } // Серия паспорта

        [JsonPropertyName("pasp_num")]
        public string PassportNo { get; set; } // Номер Паспорта
        [JsonPropertyName("pasp_vid")]
        public string PassportOrg { get; set; } // Кем выдан

        [JsonPropertyName("fk_name")]
        public string Department { get; set; } // Инстит Факультет

        [JsonPropertyName("group_name")]
        public string Group { get; set; } // Наименование Группы

        /* 
         public string WorkPlace { get; set; } // Инстиут-Факультет

         public string Post { get; set; } // По идеи (на какой специальности учиться)

         public int PagerPhone { get; set; } // Сюда рандомные 4 цифры    
         public int PagerNo { get; set; } // 0        
         public int IsBlocked { get; set; } // 0
         public DateTime ServiceBegDate { get; set; } // 11.09.2021
         public int Comment { get; set; } // 0
         public DateTime RegisterDate { get; set; } // 11.09.2021
         */


        /*<Code>55090</Code>
        <Kod>3753602263</Kod>
        <Name> Волкова Диана Сергеевна </Name>
        <Address> ЛНР Луганская область Ровеньки кв.Гагарина 4 34 </Address>
        <Category> Студент (д/о) </Category>
        <BirthDate> 08.10.2002 </BirthDate>
        <PassportSeries> ТН </PassportSeries>
        <PassportNo> 171004 </PassportNo>
        <PassportOrg> Ровеньковским ГОВД МВД ЛНР </PassportOrg>
        <WorkPlace> Факультет естественных наук </WorkPlace>
        <Post> Биология (Общая биология) </Post>
        <PagerPhone>1501</PagerPhone>
        <PagerNo> 0 </PagerNo>
        <IsBlocked> 0 </IsBlocked>
        <ServiceBegDate>11.09.2020</ServiceBegDate>
        <Comment> 0 </Comment>
        <RegisterDate>11.09.2020</RegisterDate>
        <Photo> foto\foto1.jpg </Photo>*/
    }
}