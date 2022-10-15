using ConvertAbitToXML.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConvertAbitToXML
{
    class Program
    {
        #region Переменные

        private const string ApiUrl = "https://abt.lgpu.org/api/lib/get";
        private static List<Persons> Persons { get; set; }

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
                var startupPath = Directory.GetCurrentDirectory();
                var index = 58220; // Здесь указывай номер id последнего студента в их BD
                // Получить список студентов из abt
                Persons = await ApiToAbt.GetApiPerson(ApiUrl);
                if (Persons.Count == 0)
                {
                    Console.WriteLine("Список студентов пуст");
                    return;
                }
                // Если директории нет,то создать
                if (!Directory.Exists(startupPath + "\\photo"))
                {
                    Directory.CreateDirectory(startupPath + "\\photo");
                }
                Console.WriteLine("Список студентов получен");
                // 2. Генерируем каждому img
                List<Reader> readers = new();
                
                // Паралельно выкачиваем фото
                // Рефакторинг для ускоренее выкачивания 
                await Parallel.ForEachAsync(Persons,
                    async (item, ct) => await ApiToAbt.SaveImage(item.Photo, $"{startupPath}\\photo\\{item.Code}-image.bmp", ImageFormat.Bmp));
                
                foreach (var p in Persons)
                {
                    if (p.Level != "Магистратура")
                    {
                        // Если что верни вот этот код
                        // var fullPathPhoto = $"{startupPath}\\photo\\{p.Code}-image.bmp";
                        // await ApiToAbt.SaveImage(p.Photo, fullPathPhoto, ImageFormat.Bmp);
                        p.Category = p.Category == "Очная" ? "Студент (д/о)" : "Студент (з/о)";

                        readers.Add(new Reader
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
                            Photo = $"photo/{p.Code}-image.bmp", // Записываем локальный путь photo для XML
                            RegisterDate = DateTime.Now.ToShortDateString(),
                            ServiceBegDate = DateTime.Now.ToShortDateString()
                        });
                        index++;
                    }
                    Console.WriteLine("Фотографии сгенерированы");
                }

                GenerateXML.SerializeObject(readers, Encoding.GetEncoding(1251), "One_chance.xml");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Файл создан");
            }
            catch( Exception ex)
            {
                Console.WriteLine($"Фатальная ошибка: {ex}");
            }
           
        }
    }
    
}
