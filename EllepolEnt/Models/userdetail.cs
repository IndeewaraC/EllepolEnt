using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace EllepolEnt.Models;

public class userdetail
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string NIC { get; set; }
    public int RoleID { get; set; }
}
