using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_Abpd.Entities;

[Table("Ticket_Concert")]
public class TicketConcert
{
    [Key]
    [Column("TicketConcertId")]
    public int Id {get; set;}
    
    [ForeignKey(nameof(Ticket))]
    public int TicketId {get; set;}
    
    [ForeignKey(nameof(Concert))]
    public int ConcertId {get; set;}
    
    [Column(TypeName = "numeric")]
    [Precision(10, 2)]
    public int Price {get; set;}

    public Ticket Ticket { get; set; } = null!;
    public Concert Concert { get; set; } = null!;
    
    public ICollection<PurchasedTicket> PurchasedTickets {get; set;} = null!;

}