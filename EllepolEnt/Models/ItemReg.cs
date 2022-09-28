using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class ItemReg
{
    [Key]
    [Required]
    public string itemid { get; set; }
    [Required]
    public string itemname { get; set; }
    [Required]
    public float Saleprice { get; set; }

}
