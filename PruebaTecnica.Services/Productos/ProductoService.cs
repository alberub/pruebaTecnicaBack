using PruebaTecnica.Services.Interfaces;
using PruebaTecnica.Services.Productos.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Services.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly IPruebaDbContext _pruebaDbContext;
        public ProductoService(IPruebaDbContext pruebaDbContext)
        {
            _pruebaDbContext = pruebaDbContext;
        }

        public async Task ProductoInsertar(ProductoInsertarViewModel productoInsertar)
        {
            await _pruebaDbContext.CallStoredProcedure("PTSch.PTProductoI")
                .AddParameter("@pnSku", productoInsertar.Sku)
                .AddParameter("@psArticulo", productoInsertar.Articulo)
                .AddParameter("@psMarca", productoInsertar.Marca)
                .AddParameter("@psModelo", productoInsertar.Modelo)
                .AddParameter("@pnDepartamento", productoInsertar.Departamento)
                .AddParameter("@pnClase", productoInsertar.Clase)
                .AddParameter("@pnFamilia", productoInsertar.Familia)
                .AddParameter("@pnCantidad", productoInsertar.Cantidad)
                .AddParameter("@pnStock", productoInsertar.Stock)
                .ExecuteNonQuery();
        }

        public async Task<List<ProductoActualizarViewModel>> ProductoActualizar(ProductoActualizarViewModel productoActualizar)
        {
            var producto = await _pruebaDbContext.CallStoredProcedure("PTSch.PTProductoU")
                                .AddParameter("@pnSku", productoActualizar.Sku)
                                .AddParameter("@psArticulo", productoActualizar.Articulo)
                                .AddParameter("@psMarca", productoActualizar.Marca)
                                .AddParameter("@psModelo", productoActualizar.Modelo)
                                .AddParameter("@pnDepartamento", productoActualizar.Departamento)
                                .AddParameter("@pnClase", productoActualizar.Clase)
                                .AddParameter("@pnFamilia", productoActualizar.Familia)
                                .AddParameter("@pnCantidad", productoActualizar.Cantidad)
                                .AddParameter("@pnStock", productoActualizar.Stock)
                                .AddParameter("@pnDescontinuado", productoActualizar.Descontinuado)
                                .AddParameter("@pdtFechaBaja", productoActualizar.FechaBaja)
                                .Execute<List<ProductoActualizarViewModel>>();
            return producto;
                 
        }

        public async Task<List<ProductoConsultaViewModel>> ProductoConsulta(int sku)
        {
            var producto = await _pruebaDbContext.CallStoredProcedure("PTSch.PTConsultaProductoSel")
                                .AddParameter("@pnSku", sku)
                                .Execute<List<ProductoConsultaViewModel>>();
            return producto;

        }

        public async Task ProductoBaja(int sku)
        {
            await _pruebaDbContext.CallStoredProcedure("PTSch.PTProductoDel")
                                  .AddParameter("@pnSku", sku)
                                  .ExecuteNonQuery();
        }
    }

    public interface IProductoService
    {
        Task ProductoInsertar(ProductoInsertarViewModel productoInsertar);
        Task<List<ProductoActualizarViewModel>> ProductoActualizar(ProductoActualizarViewModel productoActualizar);
        Task<List<ProductoConsultaViewModel>> ProductoConsulta(int sku);
        Task ProductoBaja(int sku);
    }
}
