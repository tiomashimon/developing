using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (var context = new OrderContext())
        {
            // a) Простий запит на вибірку
            var simpleQuery = context.Orders;

            // b) Запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN
            var specialFunctionsQuery = context.Orders
                .Where(o => o.ProductName.Contains("Product") && o.ClientCompanyName != null && o.OrderAmount > 100 && o.OrderAmount < 500);

            // c) Запит зі складним критерієм
            var complexCriteriaQuery = context.Orders
                .Where(o => o.OrderAmount > 1000 || (o.ClientCompanyName == "CompanyA" && o.CustomerLastName == "Doe"));

            // d) Запит з унікальними значеннями
            var uniqueValuesQuery = context.Orders.Select(o => o.CustomerLastName).Distinct();

            // e) Запит з використанням обчислювального поля
            var computedFieldQuery = context.Orders
                .Select(o => new { FullName = o.LastName + ", " + o.CustomerLastName, TotalAmount = o.OrderAmount * 1.1 });

            // f) Запит з групуванням по заданому полю
            var groupedQuery = context.Orders.GroupBy(o => o.ClientCompanyName);

            // g) Запит із сортуванням по заданому полю в порядку зростання та спадання значень
            var ascendingSortQuery = context.Orders.OrderBy(o => o.OrderAmount);
            var descendingSortQuery = context.Orders.OrderByDescending(o => o.OrderAmount);

            // h) Запит з використанням дій по модифікації записів
            var updateQuery = context.Orders.Where(o => o.OrderAmount < 100).ToList();
            foreach (var order in updateQuery)
            {
                order.OrderAmount += 50;
            }
            context.SaveChanges();
        }
    }
}
