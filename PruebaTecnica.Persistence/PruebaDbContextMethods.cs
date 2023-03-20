using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PruebaTecnica.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Persistence
{
    public partial class PruebaDbContext
    {
        private string StoredProcedureName { get; set; }
        private List<SqlParameter> Parameters = new List<SqlParameter>();
        private int Timeout = 30;

        public IPruebaDbContext CallStoredProcedure(string storedProcedureName)
        {
            Parameters.Clear();
            StoredProcedureName = storedProcedureName;

            return this;
        }

        public IPruebaDbContext AddParameters(Dictionary<string, object> parameters = null)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    Console.WriteLine(parameter.ToString());
                    Parameters.Add(GetParameter(parameter));
                }
            }

            return this;
        }

        public IPruebaDbContext AddParameter(string name, object value)
        {
            var sqlParameter = GetParameter(new KeyValuePair<string, object>(name, value));

            Parameters.Add(sqlParameter);

            return this;
        }

        public IPruebaDbContext GetTimeout(int Time)
        {
            Timeout = Time;
            return this;
        }

        public async Task<T> Execute<T>() where T : new()
        {

            using var command = Database.GetDbConnection().CreateCommand();

            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();

            command.CommandText = StoredProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (Parameters.Count > 0)
            {
                command.Parameters.AddRange(Parameters.ToArray());
            }

            try
            {
                using var reader = await command.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                return MapDataSetToModel<T>(dataTable);
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw (e);

            }
            finally
            {
                command.Connection.Close();
                Parameters.Clear();
            }
        }

        public async Task ExecuteNonQuery()
        {
            using var command = Database.GetDbConnection().CreateCommand();

            command.CommandTimeout = Timeout;

            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();

            command.CommandText = StoredProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (Parameters.Count > 0)
            {
                command.Parameters.AddRange(Parameters.ToArray());
            }

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                command.Connection.Close();
                Parameters.Clear();
            }
        }

        private T MapDataSetToModel<T>(DataTable dataTable) where T : new()
        {
            var jsonObject = JsonConvert.SerializeObject(dataTable);

            if (!typeof(T).FullName.Contains("System.Collections.Generic.List"))
            {
                jsonObject = jsonObject.Replace(@"[", "").Replace(@"]", "");
            }

            var objectDeserialized = JsonConvert.DeserializeObject<T>(jsonObject);

            return objectDeserialized;
        }

        private SqlParameter GetParameter(KeyValuePair<string, object> parameter)
        {
            var sqlParameter = new SqlParameter();

            if (parameter.Value == null)
            {
                sqlParameter.Value = DBNull.Value;
                sqlParameter.ParameterName = parameter.Key;
            }
            else
            {               
                if (parameter.Value.GetType() == typeof(int))
                {
                    sqlParameter.SqlDbType = SqlDbType.Int;
                }
                else if (parameter.Value.GetType() == typeof(double) || parameter.Value.GetType() == typeof(decimal))
                {
                    sqlParameter.SqlDbType = SqlDbType.Decimal;
                }
                else if (parameter.Value.GetType() == typeof(string))
                {
                    sqlParameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (parameter.Value.GetType() == typeof(DataTable))
                {
                    sqlParameter.SqlDbType = SqlDbType.Structured;
                }
                else if (parameter.Value.GetType() == typeof(bool))
                {
                    sqlParameter.SqlDbType = SqlDbType.SmallInt;
                }
                else if (parameter.Value.GetType() == typeof(byte[]))
                {
                    sqlParameter.SqlDbType = SqlDbType.Binary;
                }

                sqlParameter.Value = parameter.Value;
                sqlParameter.ParameterName = parameter.Key;
            }

            return sqlParameter;
        }

    }
}
