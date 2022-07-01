using CSVDataAccess.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataAccess
{
    public class SavedFileNameDataAccess : ISavedFileNameDataAccess
    {
        private readonly IConfiguration _configuration;

        public SavedFileNameDataAccess(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public async Task<int> SavedFileName(string FileName)
        {
            var connectionString = _configuration.GetConnectionString("develop");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("No connection string in config.json");
            int IdFileName = 0;
            await using (var con = new SqlConnection(connectionString))
            {
                await using (var cmd = new SqlCommand("pa_SavedFileName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileName", FileName);

                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    if (reader.Read())
                    {
                        IdFileName = (Int32)(await reader.IsDBNullAsync("IdFileUpload") ? 0 : Int16.Parse(reader["IdFileUpload"].ToString()));
                    }
                }
            }
            return IdFileName;
        }
    }
}
