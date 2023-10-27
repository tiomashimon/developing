using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

// Структура для представлення книги в колекції "Бібліотека"
[Serializable]
public class Book
{
    public int Code { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

// Структура для представлення читача в колекції "Читачі"
[Serializable]
public class Reader
{
    public string LastName { get; set; }
    public int BookCode { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime ReturnDate { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення колекції для "Бібліотека" і "Читачі"
        List<Book> library = new List<Book>();
        List<Reader> readers = new List<Reader>();

        // Заповнення колекції даними (приклади)
        library.Add(new Book { Code = 1, Title = "Книга 1", Author = "Автор 1", Year = 2000 });
        library.Add(new Book { Code = 2, Title = "Книга 2", Author = "Автор 2", Year = 2010 });
        library.Add(new Book { Code = 3, Title = "Книга 3", Author = "Автор 1", Year = 1995 });

        readers.Add(new Reader { LastName = "Читач 1", BookCode = 1, CheckoutDate = new DateTime(2023, 1, 1), ReturnDate = new DateTime(2023, 1, 15) });
        readers.Add(new Reader { LastName = "Читач 2", BookCode = 2, CheckoutDate = new DateTime(2023, 2, 1), ReturnDate = new DateTime(2023, 2, 15) });
        readers.Add(new Reader { LastName = "Читач 1", BookCode = 3, CheckoutDate = new DateTime(2023, 3, 1), ReturnDate = new DateTime(2023, 3, 15) });

        // a) Вивести назви книг заданого автора, видані в заданому діапазоні дат
        string authorToSearch = "Автор 1";
        DateTime startDate = new DateTime(1990, 1, 1);
        DateTime endDate = new DateTime(2005, 12, 31);

        var booksByAuthorAndDate = library
            .Where(book => book.Author == authorToSearch && book.Year >= startDate.Year && book.Year <= endDate.Year)
            .Select(book => book.Title);

        Console.WriteLine("Список книг заданого автора, виданих в заданому діапазоні дат:");
        foreach (var title in booksByAuthorAndDate)
        {
            Console.WriteLine(title);
        }

        // b) Підрахувати загальну кількість книг, які не повернуті читачами, видані у минулому столітті та вибрані у поточному році
        int currentYear = DateTime.Now.Year;
        int previousCenturyStart = currentYear - 100;

        int overdueBooksCount = readers
            .Join(library, reader => reader.BookCode, book => book.Code, (reader, book) => new { reader, book })
            .Where(data => data.reader.ReturnDate > DateTime.Now)
            .Where(data => data.book.Year < currentYear && data.book.Year >= previousCenturyStart)
            .Count();

        Console.WriteLine($"Загальна кількість неповернутих книг: {overdueBooksCount}");

        // c) Зберегти дані однієї з колекцій в XML-файлі
        string xmlFilePath = "library.xml";
        SaveCollectionToXml(library, xmlFilePath);

        Console.WriteLine($"Дані збережено у файлі {xmlFilePath}");
    }

    // Метод для збереження колекції в XML-файл
    static void SaveCollectionToXml<T>(List<T> collection, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using (XmlWriter writer = XmlWriter.Create(filePath))
        {
            serializer.Serialize(writer, collection);
        }
    }
}
