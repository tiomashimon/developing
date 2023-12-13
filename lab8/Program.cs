using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await ProcessOrdersAsync();
    }

    public static async Task ProcessOrdersAsync()
    {
        using (var context = new OrderContext())
        {
            // a) простий запит на вибірку;
            var allOrders = await context.GetOrdersAsync();

            // b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
            var specificOrders = await context.GetOrdersByProductNameAsync("SpecificProduct");

            // c) Запит зі складним критерієм;
            var complexCriteriaOrders = await context.GetOrdersWithComplexCriteriaAsync(1000, "Smith");

            // д) Запит з унікальними значеннями;
            var uniqueClientCompanies = await context.GetUniqueClientCompanyNamesAsync();

            // e) Запит з використанням обчислювального поля;
            var totalAmountPerClient = await context.GetTotalOrderAmountPerClientAsync();

            // f) Запит з групуванням по заданому полю, використовуючи умову групування;
            var groupedOrders = await context.GroupOrdersByClientAsync(5000);

            // g) Запит із сортуванням по заданому полю в порядку зростання та спадання значень;
            var ordersSortedAsc = await context.GetOrdersSortedByAmountAsync(true);
            var ordersSortedDesc = await context.GetOrdersSortedByAmountAsync(false);

            // h) Запит з використанням дій по модифікації записів.
            await context.UpdateOrderAmountAsync(1, 1500);
        }
    }
}