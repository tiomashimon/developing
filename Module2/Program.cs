class Program
{
    static void Main()
    {
        DatabaseManager dbManager = new DatabaseManager();

        // Створення таблиці
        dbManager.CreateStudentTable();

        // Додавання тестових даних
        dbManager.InsertSampleData();

        // Виведення всіх студентів
        Console.WriteLine("All students:");
        List<Student> allStudents = dbManager.GetAllStudents();
        foreach (var student in allStudents)
        {
            Console.WriteLine($"{student.LastName}, {student.Faculty}, {student.Course}, {student.Gender}, " +
                              $"{student.Scholarship}, {student.Grade1}, {student.Grade2}, {student.Grade3}");
        }

        // Виведення інформації про студента за прізвищем
        string searchLastName = "Doe";
        Console.WriteLine($"\nStudent information for {searchLastName}:");
        Student searchedStudent = dbManager.GetStudentInfoByLastName(searchLastName);
        if (searchedStudent != null)
        {
            Console.WriteLine($"{searchedStudent.LastName}, {searchedStudent.Faculty}, {searchedStudent.Course}, " +
                              $"{searchedStudent.Gender}, {searchedStudent.Scholarship}, {searchedStudent.Grade1}, " +
                              $"{searchedStudent.Grade2}, {searchedStudent.Grade3}");
        }
        else
        {
            Console.WriteLine($"Student with last name {searchLastName} not found.");
        }

        // Визначення кількості відмінників серед хлопців на заданому курсі та факультеті
        int targetCourse = 2;
        string targetFaculty = "Computer Science";
       
