using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class Invoice
{
    [Key]
    [Required]
    public string Invoice_Number { get; set; }
    [Required]
    public string Item_Id { get; set; }
    [Required]
    public string Item_Name { get; set; }
    [Required]
    public float Item_Price { get; set; }
    [Required]
    public float qty { get; set; }

}
