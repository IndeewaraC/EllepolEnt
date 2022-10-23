using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models
{
    public class PumpManagement
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string ItemID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string pumpID { get; set; }
        [Required]
        public string GRN_ID { get; set; }
        [Required]
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public float cash_In { get; set; }
        [Required]
        public float Ltrs { get; set; }
    }
}
