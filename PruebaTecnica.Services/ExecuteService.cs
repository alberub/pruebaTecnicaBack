using Newtonsoft.Json.Linq;
using PruebaTecnica.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PruebaTecnica.Services
{
    public class ExecuteService : IExecuteService
    {
        public readonly IPruebaDbContext _pruebaDbContext;

        public ExecuteService(IPruebaDbContext pruebaDbContext)
        {
            _pruebaDbContext = pruebaDbContext;
        }

        public async Task<IList<dynamic>> GetList(string storedProcedureName, JObject parameters)
        {
            var parsedParameters = parameters?.ToObject<Dictionary<string, object>>();

            var dynamicObject = await _pruebaDbContext
                                    .CallStoredProcedure(storedProcedureName)
                                    .AddParameters(parsedParameters)
                                    .Execute<List<dynamic>>();

            return dynamicObject;
        }

        public async Task<dynamic> Get(string storedProcedureName, JObject parameters)
        {
            var parsedParameters = parameters?.ToObject<Dictionary<string, object>>();

            var dynamicObject = await _pruebaDbContext
                                    .CallStoredProcedure(storedProcedureName)
                                    .AddParameters(parsedParameters)
                                    .Execute<dynamic>();

            return dynamicObject;
        }

    }

    public interface IExecuteService
    {
        Task<IList<dynamic>> GetList(string storedProcedureName, JObject parameters);

        Task<dynamic> Get(string storedProcedureName, JObject parameters);
    }
}
