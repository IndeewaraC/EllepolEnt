using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EllepolEnt.Models;

public class GRN
{
    [Key]
    [Required]
    public string GRN_ID { get; set; }
    [Required]
    [BindProperty,DataType(DataType.Date)]
    public DateTime GRN_Date { get; set; }
    [Required]
    public string Item_ID { get; set; }
    [Required]
    public float In_Price { get; set; }
    [Required]
    public int Stock_In_Amount { get; set; }

}
