using AirportIngestion.Services;
using AndreAirLines.Domain.DTO;
using System;
using System.IO;

namespace AirportIngestion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirportService airportService = new AirportService();

            Console.WriteLine("Enter the file path");
            string path = Console.ReadLine();

            if (File.Exists(path))
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string line = streamReader.ReadLine();

                    while (line != null && line != "")
                    {
                        string[] values = line.Split(';');

                        AirportData airportData = new AirportData
                        {
                            City = values[0],
                            Country = values[1],
                            Code = values[2],
                            Continent = values[3]
                        };
                        airportService.AddAirportAsync(airportData).Wait();

                        line = streamReader.ReadLine();
                    }
                }

            foreach (var item in airportService.GetAirportsAsync().Result)
            {
                Console.WriteLine(item);
            }



        }
    }
}
