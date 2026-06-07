using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium_Abpd.Entities;

[Table("Concert")]
public class Concert
{
    [Key]
    [Column("ConcertId")]
    public int Id {get; set;}
    
    [MaxLength(100)]
    public string Name {get; set;} = null!;
    
    public DateTime Date {get; set;}
    public int AvailableTickets {get; set;}
    
    public ICollection<TicketConcert> TicketConcerts {get; set;} = null!;
}