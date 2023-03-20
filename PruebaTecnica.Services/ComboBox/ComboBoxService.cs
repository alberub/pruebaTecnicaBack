using PruebaTecnica.Services.ComboBox.ViewModel;
using PruebaTecnica.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Services.ComboBox
{
    public class ComboBoxService : IComboBoxService
    {
        private readonly IPruebaDbContext _pruebaDbContext;
        public ComboBoxService(IPruebaDbContext pruebaDbContext)
        {
            _pruebaDbContext = pruebaDbContext;
        }

        public async Task<List<DepartamentoCmbViewModel>> DepartamentoCmb()
        {
            var cmb = await _pruebaDbContext.CallStoredProcedure("[PTSch].[PTCatDepartamentoCmb]")
                            .Execute<List<DepartamentoCmbViewModel>>();

            return cmb;
        }

        public async Task<List<ClaseCmbViewModel>> ClaseCmb(int numeroDepartamento)
        {
            var cmb = await _pruebaDbContext.CallStoredProcedure("[PTSch].[PTCatClaseCmb]")
                            .AddParameter("@pnDepartamento", numeroDepartamento)
                            .Execute<List<ClaseCmbViewModel>>();
            return cmb;
        }

        public async Task<List<FamiliaCmbViewModel>> FamiliaCmb(int numeroDepartamento, int numeroClase)
        {
            var cmb = await _pruebaDbContext.CallStoredProcedure("[PTSch].[PTCatFamiliaCmb]")
                            .AddParameter("@pnNumeroDepartamento", numeroDepartamento)
                            .AddParameter("@pnNumeroClase", numeroClase)
                            .Execute<List<FamiliaCmbViewModel>>();

            return cmb;
        }
        
    }

    public interface IComboBoxService
    {
        Task<List<DepartamentoCmbViewModel>> DepartamentoCmb();
        Task<List<ClaseCmbViewModel>> ClaseCmb(int numeroDepartamento);
        Task<List<FamiliaCmbViewModel>> FamiliaCmb(int numeroDepartamento, int numeroClase);
    }
}
