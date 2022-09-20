using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class tbl_Login
{
    [JsonProperty("UserID")]
    [Required]
    [Key]
    public String UserID { get; set; }

    [JsonProperty("UserName")]
    [Required]
    public String UserName { get; set; }

    [JsonProperty("Password")]
    [Required]
    public String Password { get; set; }
}
