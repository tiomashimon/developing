using System;

class Program
{
    static void Main()
    {
        string[] myArray = new string[6];

        myArray[0] = "Integer";
        myArray[1] = "Real";
        myArray[2] = "String";
        myArray[3] = "Boolean";
        myArray[4] = "Char";
        myArray[5] = "Array";

        // Виведення у прямому порядку
        Console.WriteLine("Елементи у прямому порядку:");
        foreach (string i in myArray)
        {
            Console.WriteLine(i);
        }

        // Виведення y зворотньому порядку
        Console.WriteLine("Елементи у зворотньому порядку:");
        for (int i = myArray.Length - 1; i >= 0; i--)
        {
            Console.WriteLine(myArray[i]);
        }

        // Виведення 
        Console.WriteLine("Кількість елементів у масиві: " + myArray.Length);

        // Очищення масиву 
        Array.Clear(myArray, 0, myArray.Length);
        Console.WriteLine("Масив очищений");
    }
}
