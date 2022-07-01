using CSVBusinessEntities.Wrappers;
using CSVDataAccess.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataAccess
{
    public class ListRegistersDataAccess : IListRegistersDataAccess
    {
        private readonly IConfiguration _configuration;

        public ListRegistersDataAccess(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public async Task<ICollection<Registers_Wrappers>> GetRegistersFilerByFile(int IdFile)
        {
            var connectionString = _configuration.GetConnectionString("develop");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("No connection string in config.json");
            ICollection<Registers_Wrappers> Registers = new Collection<Registers_Wrappers>();
            await using (var con = new SqlConnection(connectionString))
            {
                await using (var cmd = new SqlCommand("pa_GetRegister", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFile", IdFile);
                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    while (reader.Read())
                    {
                        Registers_Wrappers Register = new Registers_Wrappers();
                        Register.NumberColumn = (int)(await reader.IsDBNullAsync("NumberColumn") ? 0 : reader["NumberColumn"]);
                        Register.NumerRow = (int)(await reader.IsDBNullAsync("NumerRow") ? 0 : reader["NumerRow"]);
                        Register.Data = (String)(await reader.IsDBNullAsync("Data") ? null : reader["Data"]);
                        Registers.Add(Register);
                    }
                }
            }
            return Registers;
        }
    }
}
