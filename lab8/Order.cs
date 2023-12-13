using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public decimal OrderAmount { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    public string ClientCompanyName { get; set; }

    [Required]
    public string CustomerLastName { get; set; }
}