using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class Login
{

    [Key]
    public int UserID { get; set; }

    public String UserName { get; set; }

    public String Password { get; set; }


}
