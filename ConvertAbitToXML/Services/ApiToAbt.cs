using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConvertAbitToXML;

public class ApiToAbt
{
    #region Api
    // Запрос на API
    public static async Task<List<Persons>> GetApiPerson(string url)
    {
        var req = (HttpWebRequest)WebRequest.Create(url);// Создаём запрос
        req.Method = HttpMethod.Get.Method;                            // Выбираем метод запроса
        //req.Headers.Add("auth-token", token);
        req.Accept = "application/json";

        using var response = await req.GetResponseAsync();
        await using var responseStream = response.GetResponseStream();
        using StreamReader reader = new(responseStream, Encoding.UTF8);
        // Заглушка для парсинга
        var options = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString |
            JsonNumberHandling.WriteAsString
        };
        return JsonSerializer.Deserialize<List<Persons>>(await reader.ReadToEndAsync(), options);    // Возвращаем json информацию которая пришла 
    }
    #endregion

    #region Создание фото локально по url ссылки
    public static async Task SaveImage(string imageUrl, string filename, ImageFormat format)
    {
        try
        {
            // Какая-то странная фигня в бд (неужели оно реально хранит подобным образом)
            if (imageUrl == "/files/no-photo.png")
                imageUrl = "https://abt.lgpu.org/files/no-photo.png";
            
            using WebClient webClient = new();
            var stream = await webClient.OpenReadTaskAsync(new Uri(imageUrl, UriKind.Absolute));
            using Bitmap bitmap = new(stream);
            bitmap.Save(filename, format);
            await stream.FlushAsync();
            stream.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    #endregion
}

