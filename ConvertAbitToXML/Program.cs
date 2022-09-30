using ConvertAbitToXML.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.WebRequestMethods;

namespace ConvertAbitToXML
{
    class Program
    {
        #region Переменные

        private static readonly string _ApiUrl = "https://abt.lgpu.org/api/lib/get";

        private static List<Persons> _Persons;

        public static List<Persons> Persons
        {
            get => _Persons;
            set => _Persons = value;
        }

        #endregion

        // Регистрация кодировки win 1251

        // TODO: ЗАМЕНИ ArrayReaders -> LibraryExportReaders [Первый Массив]
        static async Task Main()
        {
            // на всякий случай
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding(1251);

            try
            {
                string startupPath = Directory.GetCurrentDirectory();
                int index = 58220; // Здесь указывай номер id последнего студента в их BD
                string FullPathPhoto = string.Empty;

                // Получить список студентов из abt
                Persons = await ApiToAbt.GetApiPerson(_ApiUrl);
                // 1. Список людей 
                if (Persons.Count > 0)
                {
                    Console.WriteLine("Список студентов получен");
                    // 2. Генерируем каждому img
                    List<Reader> Readers = new();
                    // Ставлю для того что у них нет primary key и нужно самому генерировать ключ
                    foreach (Persons p in Persons)
                    {
                        if (p.Level != "Магистратура")
                        {
                            // Какая-то странная фигня в бд (неужели оно реально хранит подобным образом)
                            if (p.Photo == "/files/no-photo.png")
                                p.Photo = "https://abt.lgpu.org/files/no-photo.png";
                            // Если директории нет,то создать
                            if (!Directory.Exists(startupPath + "\\photo"))
                            {
                                Directory.CreateDirectory(startupPath + "\\photo");
                            }
                            FullPathPhoto = $"{startupPath}\\photo\\{index}-image.bmp";
                            await ApiToAbt.SaveImage(p.Photo, FullPathPhoto, ImageFormat.Bmp);

                            if (p.Category == "Очная")
                                p.Category = "Студент (д/о)";
                            else
                                p.Category = "Студент (з/о)";


                            Readers.Add(new Reader
                            {
                                Code = index, // Номер читательскего билета
                                Kod = p.Kod, // ИНН
                                Name = p.FullName,// Полное имя
                                Adress = p.Adress,// Адрес
                                BirthDate = p.BirthDate.ToShortDateString(),// Дата рождения
                                PassportSeries = p.PassportSeries, // Серия паспорта
                                PassportNo = p.PassportNo, //Номер паспорта 
                                PassportOrg = p.PassportOrg, // Кем выдан
                                WorkPlace = p.Department, // Институт, Факультет
                                Post = p.Group, // Наименование группы
                                PagerPhone = 1517, // Неактуальное старьё
                                Category = p.Category, // Форма обучения
                                Photo = $"photo/{index}-image.bmp", // Записываем локальный путь photo для XML
                                RegisterDate = DateTime.Now.ToShortDateString(),
                                ServiceBegDate = DateTime.Now.ToShortDateString()
                            });
                            index++;
                        }
                        Console.WriteLine("Фотографии сгенерированы");
                        // 3. Проводим Сереализацию
                        //GenerateXML.SerializeToXml(Readers, "reader.xml");    
                    }

                    GenerateXML.SerializeObject(Readers, Encoding.GetEncoding(1251), "One_chance.xml");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Файл создан");
                }
                else Console.WriteLine("Список студентов пуст");
                
               

            }
            catch( Exception ex)
            {
                Console.WriteLine($"Фатальная ошибка: {ex}");
            }
           
        }
    }
    
}
