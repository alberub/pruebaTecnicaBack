using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PruebaTecnica.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnica.WebApi.Controllers
{
    [ApiExplorerSettings(GroupName = "General")]
    public class ExecuteController : BaseController
    {
        private readonly IExecuteService _executeService;
        public ExecuteController(IExecuteService executeService)
        {
            _executeService = executeService;
        }
        [HttpGet]
        public async Task<ActionResult<IList<dynamic>>> GetList(string storedProcedure, string parameters)
        {
            var parsedParameters = parameters == null ? null : JsonConvert.DeserializeObject<JObject>(parameters);

            var dynamicListObject = await _executeService.GetList(storedProcedure, parsedParameters);

            return Ok(dynamicListObject);
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> Get(string storedProcedure, string parameters)
        {
            var parsedParameters = parameters == null ? null : JsonConvert.DeserializeObject<JObject>(parameters);

            var dynamicObject = await _executeService.Get(storedProcedure, parsedParameters);

            return Ok(dynamicObject);
        }
    }
}
