using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Приклад даних: створення колекцій "Students" та "ParticipationInClubs"
        var Students = new List<Student>
        {
            new Student { StudentCode = 1, LastName = "Шимон", Course = 1 },
            new Student { StudentCode = 2, LastName = "Півкач", Course = 2 },
            new Student { StudentCode = 3, LastName = "Довганич", Course = 2 },
            // Додайте інших студентів
        };

        var ParticipationInClubs = new List<Participation>
        {
            new Participation { StudentCode = 1, ClubName = "Програмування", Year = 2023 },
            new Participation { StudentCode = 2, ClubName = "Музика", Year = 2023 },
            new Participation { StudentCode = 2, ClubName = "Програмування", Year = 2023 },
            new Participation { StudentCode = 3, ClubName = "Програмування", Year = 2023 },
            // Додайте інші участі у гуртках
        };

        // Запит 1: Вивести назви гуртків та кількість їх учасників
        var groups = ParticipationInClubs
            .GroupBy(participation => participation.ClubName)
            .Select(g => new { ClubName = g.Key, NumberOfParticipants = g.Count() });

        Console.WriteLine("Запит 1: Назви гуртків та кількість учасників:");
        foreach (var group in groups)
        {
            Console.WriteLine($"Назва гуртка: {group.ClubName}, Кількість учасників: {group.NumberOfParticipants}");
        }

        // Запит 2: Вивести кількість студентів заданого курсу, які займаються у гуртку із заданою назвою
        int selectedCourse = 2; // Задайте потрібний курс
        string selectedClubName = "Гурток2"; // Задайте потрібну назву гуртка

        var studentCount = Students
            .Count(student => student.Course == selectedCourse &&
                              ParticipationInClubs.Any(participation => participation.StudentCode == student.StudentCode &&
                                                                      participation.ClubName == selectedClubName));

        Console.WriteLine($"\nЗапит 2: Кількість студентів на курсі {selectedCourse}, які займаються у гуртку '{selectedClubName}': {studentCount}");

        // Запит 3: Вивести топ-3 гуртки з найбільшою кількістю другокурсників у поточному році
        int currentYear = DateTime.Now.Year;

        var topClubs = ParticipationInClubs
            .Where(participation => participation.Year == currentYear)
            .GroupBy(participation => participation.ClubName)
            .Select(g => new { ClubName = g.Key, NumberOfSecondYearStudents = g.Count(participation => Students.Any(student => student.StudentCode == participation.StudentCode && student.Course == 2)) })
            .OrderByDescending(g => g.NumberOfSecondYearStudents)
            .Take(3);

        Console.WriteLine("\nЗапит 3: Топ-3 гуртки з найбільшою кількістю другокурсників у поточному році:");
        foreach (var club in topClubs)
        {
            Console.WriteLine($"Назва гуртка: {club.ClubName}, Кількість другокурсників: {club.NumberOfSecondYearStudents}");
        }
    }
}

class Student
{
    public int StudentCode { get; set; }
    public string LastName { get; set; }
    public int Course { get; set; }
}

class Participation
{
    public int StudentCode { get; set; }
    public string ClubName { get; set; }
    public int Year { get; set; }
}
