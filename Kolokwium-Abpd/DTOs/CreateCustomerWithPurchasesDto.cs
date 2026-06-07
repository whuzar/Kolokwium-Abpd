namespace Kolokwium_Abpd.DTOs;

public class CreateCustomerWithPurchasesDto
{
    public CreateCustomerDto Customer { get; set; } = null!;
    public ICollection<CreatePurchaseDto> Purchases { get; set; } = null!;
}

public class CreateCustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}

public class CreatePurchaseDto
{
    public string SeatNumber { get; set; } = null!;
    public string ConcertNumber { get; set; } = null!;
    public double Price { get; set; }
}