using System;
using System.Xml;

class Program
{
    static void Main()
    {
        // Створення нового документа XML
        XmlDocument xmlDoc = new XmlDocument();

        // Створення кореневого елемента
        XmlElement root = xmlDoc.CreateElement("Aeroport");

        // Створення елементів для кожного рейсу
        for (int i = 1; i <= 3; i++)
        {
            XmlElement flight = xmlDoc.CreateElement("Flight");
            flight.SetAttribute("Number", $"Flight{i}");
            flight.SetAttribute("Destination", $"City{i}");
            flight.SetAttribute("Seats", $"{100 + i}");
            flight.SetAttribute("Duration", $"{i} hours");
            flight.SetAttribute("Price", $"{1000 + i * 100}");

            root.AppendChild(flight);
        }

        xmlDoc.AppendChild(root);

        // Збереження документа в файл "Aeroport.xml"
        xmlDoc.Save("Aeroport.xml");

        Console.WriteLine("Файл Aeroport.xml створено та заповнено даними.");

        // Перегляд файлу на консолі
        Console.WriteLine("Перегляд файлу Aeroport.xml:");
        xmlDoc.Load("Aeroport.xml");
        XmlNodeList flights = xmlDoc.SelectNodes("/Aeroport/Flight");
        foreach (XmlNode flight in flights)
        {
            Console.WriteLine($"Номер рейсу: {flight.Attributes["Number"].Value}");
            Console.WriteLine($"Місто прибуття: {flight.Attributes["Destination"].Value}");
            Console.WriteLine($"Кількість місць: {flight.Attributes["Seats"].Value}");
            Console.WriteLine($"Час перельоту: {flight.Attributes["Duration"].Value}");
            Console.WriteLine($"Ціна білету: {flight.Attributes["Price"].Value} грн");
            Console.WriteLine();
        }

        // Визначення кількості рейсів з ціною білету більше X грн
        int priceThreshold = 1500;
        flights = xmlDoc.SelectNodes($"/Aeroport/Flight[@Price > {priceThreshold}]");
        Console.WriteLine($"Кількість рейсів з ціною білету більше {priceThreshold} грн: {flights.Count}");

        // Визначення загальної кількості пасажирів, які летять в місто Y рейсами, час перельоту яких не більше 5 годин
        string destinationCity = "City2";
        int maxDurationHours = 5;
        flights = xmlDoc.SelectNodes($"/Aeroport/Flight[@Destination='{destinationCity}' and number(substring(@Duration, 1, 1)) <= {maxDurationHours}]");
        int totalPassengers = 0;
        foreach (XmlNode flight in flights)
        {
            int seats = int.Parse(flight.Attributes["Seats"].Value);
            totalPassengers += seats;
        }
        Console.WriteLine($"Загальна кількість пасажирів, які летять в місто {destinationCity} рейсами з часом перельоту не більше {maxDurationHours} годин: {totalPassengers}");
    }
}
