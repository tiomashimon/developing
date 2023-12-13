using System;
using Npgsql;

class Program
{
    static void Main()
    {
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter order amount: ");
        decimal orderAmount = decimal.Parse(Console.ReadLine());

        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();

        Console.Write("Enter client company name: ");
        string clientCompanyName = Console.ReadLine();

        Console.Write("Enter customer last name: ");
        string customerLastName = Console.ReadLine();

       
        string query = "INSERT INTO orders (last_name, order_amount, product_name, client_company_name, customer_last_name) " +
                       "VALUES (@LastName, @OrderAmount, @ProductName, @ClientCompanyName, @CustomerLastName)";

        DatabaseHelper.ExecuteQuery(query,
            new NpgsqlParameter("@LastName", lastName),
            new NpgsqlParameter("@OrderAmount", orderAmount),
            new NpgsqlParameter("@ProductName", productName),
            new NpgsqlParameter("@ClientCompanyName", clientCompanyName),
            new NpgsqlParameter("@CustomerLastName", customerLastName));

        Console.WriteLine("Order added successfully!");
    }
}
