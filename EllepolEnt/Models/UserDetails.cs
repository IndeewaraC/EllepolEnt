using System.ComponentModel.DataAnnotations;

namespace EllepolEnt.Models;

public class UserDetails
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string NIC { get; set; }
    public int RoleID { get; set; }
}
