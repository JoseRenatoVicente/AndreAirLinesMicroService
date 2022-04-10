using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirportDatasPerformace
{
    internal class Program
    {
        private static int errors = 0;
        private static int sucess = 0;
        private static string eftype = "";
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            Console.WriteLine(@"

                                1) EF 
                                2) EFRaw
                                ------------------------------
                                0) - Exit
");

            string option = Console.ReadLine();

            switch (option)
            {
                case "0": Environment.Exit(0); break;

                case "1":
                    Console.Clear();
                    eftype = "EF";
                    RunEFAsync().Wait();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    eftype = "EFRaw";
                    RunEFAsync().Wait();
                    BackMenu();
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    BackMenu();
                    break;
            }
        }

        public static void BackMenu()
        {
            Console.WriteLine("\n Press any key to return to the menu...");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        public async static Task RunEFAsync()
        {
            var sw = new Stopwatch();
            Console.WriteLine("\nDapper");
            sw.Start();
            for (var i = 0; i < 500; i++) await GetAPIDapper();
            sw.Stop();
            Console.WriteLine("Time: " + sw.Elapsed);
            Console.WriteLine("Erros: " + errors);
            Console.WriteLine("Sucess: " + sucess);
            Clear();
            sw.Restart();
            for (var i = 0; i < 500; i++) await GetAPIEF();
            sw.Stop();
            Console.WriteLine("\n" + eftype);
            Console.WriteLine("Time: " + sw.Elapsed);
            Console.WriteLine("Erros: " + errors);
            Console.WriteLine("Sucess: " + sucess);
            Clear();
        }

        public async static Task GetAPIDapper()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44395");
                    HttpResponseMessage response = await client.GetAsync("api/AirportDatas");

                    if (response.IsSuccessStatusCode)
                        sucess++;
                }
                catch (Exception)
                {
                    errors++;
                }
            }
        }

        public async static Task GetAPIEF()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44368");
                    HttpResponseMessage response = await client.GetAsync("api/AirportDatas/" + eftype);

                    if (response.IsSuccessStatusCode)
                        sucess++;
                }
                catch (Exception)
                {
                    errors++;
                }
            }
        }

        public static void Clear()
        {
            errors = 0;
            sucess = 0;

        }
    }
}
