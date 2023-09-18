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


            
            int[] numbers = { 10, 5, 8, 2, 7, 1 };

            
            int minElement = numbers[0];
            int minIndex = 0;

           
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < minElement)
                {
                    
                    minElement = numbers[i];
                    minIndex = i;
                }
            }

            
            Console.WriteLine($"Мінімальний елемент: {minElement}");
            Console.WriteLine($"Порядковий номер мінімального елемента: {minIndex}");
        
    

}
}
