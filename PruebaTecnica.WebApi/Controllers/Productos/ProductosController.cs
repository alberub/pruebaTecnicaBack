using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Services.Productos;
using PruebaTecnica.Services.Productos.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnica.WebApi.Controllers.Productos
{
    [ApiExplorerSettings(GroupName = "Productos")]
    public class ProductosController : BaseController
    {
        private readonly IProductoService _productoService;
        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ProductoInsertar([FromBody] ProductoInsertarViewModel productoInsertar)
        {
            await _productoService.ProductoInsertar(productoInsertar);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductoActualizarViewModel>> ProductoActualizar([FromBody] ProductoActualizarViewModel productoActualizar)
        {
            var producto = await _productoService.ProductoActualizar(productoActualizar);
            return producto;
        }

        [HttpGet("{sku}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProductoConsultaViewModel>> ProductoConsulta(int sku)
        {
            var prodConsulta = await _productoService.ProductoConsulta(sku);
            return prodConsulta;
        }

        [HttpDelete("{sku}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task ProductoBaja(int sku)
        {
            await _productoService.ProductoBaja(sku);
        }
    }
}
