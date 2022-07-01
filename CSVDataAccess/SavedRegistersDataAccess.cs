using CSVDataAccess.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CSVDataAccess
{
    public class SavedRegistersDataAccess : ISavedRegistersDataAccess
    {
        private readonly IConfiguration _configuration;

        public SavedRegistersDataAccess(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public async void SaveRegisters(DataTable Table)
        {
            var connectionString = _configuration.GetConnectionString("develop");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("No connection string in config.json");
            await using (var con = new SqlConnection(connectionString))
            {
                SqlBulkCopy objbulk = new SqlBulkCopy(con);
                objbulk.DestinationTableName = "Registers";
                objbulk.ColumnMappings.Add("IdUpload", "IdUpload");
                objbulk.ColumnMappings.Add("Data", "Data");
                objbulk.ColumnMappings.Add("NumberColumn", "NumberColumn");
                objbulk.ColumnMappings.Add("NumerRow", "NumerRow");
                objbulk.ColumnMappings.Add("DateCreation", "DateCreation");
                con.Open();
                objbulk.WriteToServer(Table);
            }
        }
    }
}
