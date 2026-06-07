using Kolokwium_Abpd.Data;
using Kolokwium_Abpd.DTOs;
using Kolokwium_Abpd.Entities;
using Kolokwium_Abpd.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_Abpd.Services;

public class DbService : IDbService
{
    
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<PurchasesDto> GetPurchasesById(int id)
    {
        var purchases = await _context.PurchasedTickets.Where(p => p.CustomerId == id)
            .Select(p => new PurchaseDto
            {
                Date = p.PurchaseDate,
                Price = p.TicketConcert.Price,
                Ticket = new TicketDto()
                {
                    Serial = p.TicketConcert.Ticket.SerialNumber,
                    SeatNumber = p.TicketConcert.Ticket.SeatNumber
                },
                Concert = new ConcertDto()
                {
                    Name = p.TicketConcert.Concert.Name,
                    Date =  p.TicketConcert.Concert.Date,
                }
                
            })
            .ToListAsync();   
        
        var purchasesDto = await _context.Customers.Where(c => c.Id == id)
            .Select(c => new PurchasesDto()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Purchases = purchases
            }).FirstOrDefaultAsync();

        if (purchasesDto == null)
        {
            throw new NotFoundException("Purchases not found");   
        }
        
        return purchasesDto;
    }

    public async Task CreateCustomerWithPurchases(CreateCustomerWithPurchasesDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == request.Customer.Id);

            if (customer != null)
            {
                throw new AlreadyExistException("Customer already exists.");
            }

            Customer newCustomer = new Customer()
            {
                Id = request.Customer.Id,
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                PhoneNumber = request.Customer.PhoneNumber
            };
            
            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        } 
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
}