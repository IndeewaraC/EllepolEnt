using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class Stock
{
    [Key]
    [Required]
    public string Itemid { get; set; }
    [Required]
    public int Available_Stock { get; set; }
}
