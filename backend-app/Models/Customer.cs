using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models;

[Table("Customer")]
public class Customer {
    public int Id { get; set; }
    public string First { get; set; }
    public string Last { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }
    public string Country { get; set; }
    public DateTime CreatedAt { get; set; }
}
