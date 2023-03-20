using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Services.Productos.ViewModel
{
    public class ProductoActualizarViewModel
    {
        public int Sku { get; set; }
        public string Articulo { get; set; }
        public string Marca { get; set;}
        public string Modelo { get; set;}
        public int Departamento { get; set;}
        public int Clase { get; set; }
        public int Familia { get; set; }
        public int Cantidad { get; set; }
        public int Stock { get; set; }
        public int Descontinuado { get; set; }
        public string FechaBaja { get; set; }
    }
}
