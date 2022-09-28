using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class GRN
{
    [Key]
    [Required]
    public string GRN_ID { get; set; }
    [Required]
    public DateTime GRN_Date { get; set; }
    [Required]
    public string Item_ID { get; set; }
    [Required]
    public float In_Price { get; set; }
    [Required]
    public int Stock_In_Amount { get; set; }

}
