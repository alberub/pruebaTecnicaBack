using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Services.ComboBox;
using PruebaTecnica.Services.ComboBox.ViewModel;
using PruebaTecnica.Services.Productos;
using PruebaTecnica.Services.Productos.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnica.WebApi.Controllers.ComboBox
{
    [ApiExplorerSettings(GroupName = "ComboBox")]
    public class ComboBoxController : BaseController
    {
        private readonly IComboBoxService _comboBoxService;
        public ComboBoxController(IComboBoxService comboBoxService)
        {
            _comboBoxService = comboBoxService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<DepartamentoCmbViewModel>> DepartamentoConsulta()
        {
            var comboDepart = await _comboBoxService.DepartamentoCmb();
            return comboDepart;
        }

        [HttpGet("{departamento}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ClaseCmbViewModel>> ClaseConsulta(int departamento)
        {
            var comboClase = await _comboBoxService.ClaseCmb(departamento);
            return comboClase;
        }

        [HttpGet("{departamento}/{clase}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FamiliaCmbViewModel>> FamiliaConsulta(int departamento, int clase)
        {
            var comboFamilia = await _comboBoxService.FamiliaCmb(departamento, clase);
            return comboFamilia;
        }
    }
}
