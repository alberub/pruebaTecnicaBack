using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Services.Interfaces
{
    public interface IPruebaDbContext
    {
        IPruebaDbContext CallStoredProcedure(string storedProcedureName);
        IPruebaDbContext AddParameter(string name, object value);
        IPruebaDbContext AddParameters(Dictionary<string, object> parameters = null);
        Task<T> Execute<T>() where T : new();
        Task ExecuteNonQuery();
        IPruebaDbContext GetTimeout(int Time);
    }
}
