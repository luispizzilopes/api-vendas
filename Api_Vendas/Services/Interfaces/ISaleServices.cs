using Api_Vendas.DTOs;
using Api_Vendas.Models;

namespace Api_Vendas.Services.Interfaces
{
    public interface ISaleServices
    {
        Task<IEnumerable<Sale>> GetSales();
        Task<bool> CreateSale (CreateSaleDTO sale);
        Task<bool> CancelSale(int id);
        Task<bool> AddItem(AddItemSaleDTO item);
        Task<bool> RemoveItem(RemoveItemSaleDTO item); 
    }
}
