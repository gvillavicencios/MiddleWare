using System;
using System.Net;

namespace SendData
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new WebClient())
            {
                //URl de Servicio Local
                var url = "http://localhost:9399/";
                byte[] data = new byte[1];
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                
                Console.Clear();
                Console.WriteLine("========================");
                Console.WriteLine("Validador de MiddleWare");
                Console.WriteLine("========================");
                Console.WriteLine("\n");
                Console.WriteLine("Resultado sin clave-usuario en el header ==>");
                Console.WriteLine("\n");
                /*Prueba de Usuario sin Valor*/
                client.Headers.Add("usuario", "Juan");
                try
                {
                    var response_fail = client.DownloadString(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Mensaje de Response: \n");
                    Console.WriteLine("Ocurrio un error");
                    Console.WriteLine(e.Message.ToString());
                }
                client.Headers.Remove("usuario");
                Console.WriteLine("\n");
                Console.WriteLine("Resultado con valor clave-usuario invalido ==>");
                Console.WriteLine("\n");

                /*Prueba de Usuario con clave erronea en JSON*/                
                client.Headers.Add("usuario", "Hector");
                client.Headers.Add("clave", "123456");
                try
                {
                    var response_invalid = client.DownloadString(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Mensaje de Response: \n");
                    Console.WriteLine("Ocurrió un error");
                    Console.WriteLine(e.Message.ToString());
                }               
                Console.WriteLine("\n");

                /*Prueba de Usuario con clave valida en el JSON*/
                Console.WriteLine("Resultado con valor clave-usuario invalido ==>");
                Console.WriteLine("\n");
                client.Headers.Remove("usuario");
                client.Headers.Remove("clave");
                client.Headers.Add("usuario", "Juan");
                client.Headers.Add("clave", "abc123");
                var response_valid = client.DownloadString(url);
                Console.WriteLine(response_valid);


                Console.ReadLine();

            }
        }
    }
}
