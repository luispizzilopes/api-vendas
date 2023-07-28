using Api_Vendas.Context;
using Api_Vendas.DTOs;
using Api_Vendas.Models;
using Api_Vendas.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_Vendas.Services
{
    public class SaleServices : ISaleServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper; 

        public SaleServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddItem(AddItemSaleDTO addItemSale)
        {
            var sale = await _context.Sales
                .Where(sale => sale.Id == addItemSale.IdSale)
                .Include(x => x.Items)
                .FirstOrDefaultAsync();

            var item = await _context.Items.FindAsync(addItemSale.CodItem);

            if(sale != null && item != null)
            {
                sale.Items.Add(item);
                sale.Price += item.Price;
                await _context.SaveChangesAsync();
                return true; 
            }

            return false;
        }

        public async Task<bool> CancelSale(int id)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id); 
            
            if (sale != null) 
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
                return true;
            }

            return false; 
        }

        public async Task<bool> CreateSale(CreateSaleDTO createSaleDTO)
        {
            if(createSaleDTO != null)
            {
                var sale = _mapper.Map<Sale>(createSaleDTO); 
                await _context.Sales.AddAsync(sale); 
                await _context.SaveChangesAsync();
                return true;
            }

            return false; 
        }

        public async Task<IEnumerable<Sale>> GetSales()
        {
            var sales = await _context.Sales
                .Include(s => s.Items)
                .ToListAsync();

            return sales;
        }

        public async Task<bool> RemoveItem(RemoveItemSaleDTO removeItemSale)
        {
            var sale = await _context.Sales
                .Where(sale => sale.Id == removeItemSale.IdSale)
                .Include(x => x.Items)
                .FirstOrDefaultAsync();

            var item = await _context.Items.FindAsync(removeItemSale.CodItem);

            if (sale != null && item != null)
            {
                sale.Items.Remove(item);
                sale.Price -= item.Price;
                await _context.SaveChangesAsync();
                return true; 
            }

            return false; 
        }
    }
}
