using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class Pumpregistration
{
    [Key]
    [Required]
    public string PumpID { get; set; }
    [Required]
    public string pumpname { get; set; }
}
