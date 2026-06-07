using Kolokwium_Abpd.DTOs;

namespace Kolokwium_Abpd.Services;

public interface IDbService
{
    Task<PurchasesDto> GetPurchasesById(int id);
    Task CreateCustomerWithPurchases(CreateCustomerWithPurchasesDto request);
}