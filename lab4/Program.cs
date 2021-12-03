using System;
using System.IO;
using System.Net;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            Console.WriteLine("Введите путь к файлу. Пример: ftp://ftp.mccme.ru/pub/metapost.README");
            path = Console.ReadLine();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // если требуется логин и пароль, устанавливаем их
            //request.Credentials = new NetworkCredential("login", "password");
            //request.EnableSsl = true; // если используется ssl

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            // сохраняем файл
            FileStream fs = new FileStream("fileTest.txt", FileMode.Create);

            byte[] buffer = new byte[64];
            int size = 0;
            while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, size);
            }
            fs.Close();
            response.Close();

            Console.WriteLine("Загрузка и сохранение файла завершены");
            Console.ReadKey();
        }
    }
}
