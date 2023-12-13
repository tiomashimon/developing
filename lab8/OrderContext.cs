using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgress;Password=postgrespassword;Database=dblab");
    }
     // a) простий запит на вибірку;
    public async Task<List<Order>> GetOrdersAsync()
    {
        return await Orders.ToListAsync();
    }
    // b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
    public async Task<List<Order>> GetOrdersByProductNameAsync(string productName)
    {
        return await Orders.Where(order => order.ProductName == productName).ToListAsync();
    }

    // c) Запит зі складним критерієм;
    public async Task<List<Order>> GetOrdersWithComplexCriteriaAsync(decimal minOrderAmount, string customerLastName)
    {
        return await Orders.Where(order => order.OrderAmount > minOrderAmount && order.CustomerLastName == customerLastName)
                           .ToListAsync();
    }

    // d) Запит з унікальними значеннями;
    public async Task<List<string>> GetUniqueClientCompanyNamesAsync()
    {
        return await Orders.Select(order => order.ClientCompanyName).Distinct().ToListAsync();
    }

    // e) Запит з використанням обчислювального поля;
    public async Task<List<decimal>> GetTotalOrderAmountPerClientAsync()
    {
        return await Orders.GroupBy(order => order.ClientCompanyName)
                           .Select(group => group.Sum(order => order.OrderAmount))
                           .ToListAsync();
    }

    // f) Запит з групуванням по заданому полю, використовуючи умову групування;
    public async Task<List<IGrouping<string, Order>>> GroupOrdersByClientAsync(decimal minOrderAmount)
    {
        return await Orders.GroupBy(order => order.ClientCompanyName)
                           .Where(group => group.Sum(order => order.OrderAmount) > minOrderAmount)
                           .ToListAsync();
    }

    // g) Запит із сортуванням по заданому полю в порядку зростання та спадання значень;
    public async Task<List<Order>> GetOrdersSortedByAmountAsync(bool ascending)
    {
        return ascending
            ? await Orders.OrderBy(order => order.OrderAmount).ToListAsync()
            : await Orders.OrderByDescending(order => order.OrderAmount).ToListAsync();
    }

    // h) Запит з використанням дій по модифікації записів.
    public async Task UpdateOrderAmountAsync(int orderId, decimal newAmount)
    {
        var order = await Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order != null)
        {
            order.OrderAmount = newAmount;
            await SaveChangesAsync();
        }
    }
}