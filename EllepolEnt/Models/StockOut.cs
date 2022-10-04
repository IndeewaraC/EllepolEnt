using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class StockOut
{
    [Key]
    [Required]
    public string Stock_ID { get; set; }
    [Required]
    [BindProperty, DataType(DataType.Date)]
    public DateTime stockoutDate { get; set; }
    [Required]
    public string ItemId { get; set; }
    [Required]
    public float Stockout { get; set; }
    [Required]
    public float StockoutPrice { get; set; }

}
