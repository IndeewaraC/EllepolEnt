using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class Invoice
{
    [Key]
    [Required]
    public string InvoiceNumber { get; set; }
    [Required]
    public string Item_Id { get; set; }
    [Required]
    public string ItemName { get; set; }
    [Required]
    public float ItemPrice { get; set; }
    [Required]
    public float DiscountRate { get; set; }
    [Required]
    public float qty { get; set; }

}
