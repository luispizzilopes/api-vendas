using Api_Vendas.DTOs;
using Api_Vendas.Models;
using Api_Vendas.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleServices _saleServices; 

        public SaleController(ISaleServices saleServices)
        {
            _saleServices = saleServices;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IAsyncEnumerable<Sale>>> GetSales()
        {
            try
            {
                return Ok(await _saleServices.GetSales()); 
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível retornar a lista de vendas!");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateSale([FromBody] CreateSaleDTO sale)
        {
            try
            {
                if (await _saleServices.CreateSale(sale))
                {
                    return Ok("Venda criada com sucesso!");
                }
                else
                {
                    return BadRequest("Não foi possível gerar uma nova venda");
                }
                
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro durante a execução do EndPoint, tente novamente!");
            }
        }

        [HttpPost("additem")]
        public async Task<ActionResult> AddItem([FromBody] AddItemSaleDTO addItemSale)
        {
            try
            {
                if(await _saleServices.AddItem(addItemSale))
                {
                    return Ok($"Item {addItemSale.CodItem} adicionado com sucesso na venda {addItemSale.IdSale}");
                }
                else
                {
                    return BadRequest("Verifique todos os campos e tente novamente!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro durante a execução do EndPoint, tente novamente!"); 
            }
        }

        [HttpDelete("cancel")]
        public async Task<ActionResult> CancelSale([FromQuery] int id)
        {
            try
            {
                if (await _saleServices.CancelSale(id))
                {
                    return Ok("Venda cancelada com sucesso!");
                }
                else
                {
                    return BadRequest("Não foi possível cancelar a venda!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro durante a execução do EndPoint, tente novamente!");
            }
        }

        [HttpDelete("removeitem")]
        public async Task<ActionResult> RemoveItem([FromBody] RemoveItemSaleDTO removeItemSale)
        {
            try
            {
                if(await _saleServices.RemoveItem(removeItemSale))
                {
                    return Ok($"Item {removeItemSale.CodItem} removido com sucesso da venda {removeItemSale.IdSale}"); 
                }
                else
                {
                    return BadRequest("Verifique todos os campos e tente novamente!"); 
                }
                
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro durante a execução do EndPoint, tente novamente!");
            }
        }
    }
}
