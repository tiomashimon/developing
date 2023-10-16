using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Flight
{
    public string FlightNumber { get; set; }
    public string DestinationCity { get; set; }
    public int SeatCount { get; set; }
    public TimeSpan FlightDuration { get; set; }
    public decimal TicketPrice { get; set; }
}

class Program
{
    static void Main()
    {
        // Створення списку рейсів
        List<Flight> flights = new List<Flight>
        {
            new Flight
            {
                FlightNumber = "AB123",
                DestinationCity = "New York",
                SeatCount = 200,
                FlightDuration = TimeSpan.FromHours(6),
                TicketPrice = 500.0M
            },
            new Flight
            {
                FlightNumber = "CD456",
                DestinationCity = "Paris",
                SeatCount = 150,
                FlightDuration = TimeSpan.FromHours(3),
                TicketPrice = 400.0M
            },
            
        };

        // Запис списку рейсів у файл "Aeroport.json"
        File.WriteAllText("Aeroport.json", JsonConvert.SerializeObject(flights, Formatting.Indented));

        // Зчитування файлу та виведення його вмісту на консолі
        string json = File.ReadAllText("Aeroport.json");
        Console.WriteLine("JSON-файл 'Aeroport.json' містить наступну інформацію:");
        Console.WriteLine(json);

        // Розбір JSON-файлу та виконання аналізу
        var parsedFlights = JsonConvert.DeserializeObject<List<Flight>>(json);

        // Визначення кількості рейсів з ціною білету більше за Х грн
        decimal targetPrice = 450.0M; // Приклад значення Х
        int flightsWithHighPrice = parsedFlights.Count(flight => flight.TicketPrice > targetPrice);
        Console.WriteLine($"Кількість рейсів з ціною білету більше {targetPrice} грн: {flightsWithHighPrice}");

        // Визначення загальної кількості пасажирів на рейсах в місто Y з часом перельоту не більше 5 годин
        string destinationCityY = "Paris"; // Приклад значення Y
        TimeSpan maxFlightDuration = TimeSpan.FromHours(5);
        int totalPassengersToCityY = parsedFlights
            .Where(flight => flight.DestinationCity == destinationCityY && flight.FlightDuration <= maxFlightDuration)
            .Sum(flight => flight.SeatCount);
        Console.WriteLine($"Загальна кількість пасажирів, які летять в місто {destinationCityY} з часом перельоту не більше 5 годин: {totalPassengersToCityY}");
    }
}

