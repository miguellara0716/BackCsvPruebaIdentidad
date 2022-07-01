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
    public class ListFilesDataAccess : IListFilesDataAccess
    {
        private readonly IConfiguration _configuration;

        public ListFilesDataAccess(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public async Task<ICollection<FileName_Wrapper>> GetFilesNames()
        {
            var connectionString = _configuration.GetConnectionString("develop");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("No connection string in config.json");
            ICollection<FileName_Wrapper> FilesNames = new Collection<FileName_Wrapper>();
            await using (var con = new SqlConnection(connectionString))
            {
                await using (var cmd = new SqlCommand("pa_ObtenerFileUploads", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    while (reader.Read())
                    {
                        FileName_Wrapper FileName = new FileName_Wrapper();
                        FileName.IdFileUpload = (int)(await reader.IsDBNullAsync("IdFileUpload") ? 0 : reader["IdFileUpload"]);
                        FileName.FileName = (String)(await reader.IsDBNullAsync("FileName") ? null : reader["FileName"]);
                        FilesNames.Add(FileName);
                    }
                }
            }
            return FilesNames;
        }

    }
}
