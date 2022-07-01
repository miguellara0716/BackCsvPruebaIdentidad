using CSVBusiness.Interface;
using CSVBusinessEntities.Wrappers;
using CSVDataAccess.Interface;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVBusiness
{
    public class SaveFileBusiness : ISaveFileBusiness
    {
        private readonly ISavedFileNameDataAccess _fileNameData;
        private readonly ISavedRegistersDataAccess _registersData;
        public SaveFileBusiness(ISavedFileNameDataAccess FileNameData, ISavedRegistersDataAccess RegistersData)
        {
            _fileNameData = FileNameData;
            _registersData = RegistersData;
        }

        public async void SavedFile (SaveItem_Wrapper DataFile)
        {

            var base64EncodedBytes = Convert.FromBase64String(DataFile.File);
            var encoded = Encoding.UTF8.GetString(base64EncodedBytes);
            TextReader textReader = new StringReader(encoded);
            string Stringdata = textReader.ReadToEnd();
            var ArrayData = Stringdata.Split(",");          
            int row = 0;
            int Column = 0;
            int IdFileName = await _fileNameData.SavedFileName(DataFile.FileName);
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("IdUpload", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("NumerRow", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("NumberColumn", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Data", typeof(string)));
            tbl.Columns.Add(new DataColumn("DateCreation", typeof(DateTime)));
            foreach (string data in ArrayData)
            {
                DataRow dr = tbl.NewRow();
                if (data.Contains("\n"))
                {
                    var dataspliting = data.Split("\n");
                    dr["IdUpload"] = IdFileName;
                    dr["Data"] = dataspliting.ElementAt(0);
                    dr["NumberColumn"] = Column;
                    dr["NumerRow"] = row;
                    dr["DateCreation"] = DateTime.Now;
                    tbl.Rows.Add(dr);
                    Column = 0;
                    row++;
                    if (!String.IsNullOrEmpty(dataspliting.ElementAt(1)))
                    {
                        dr = tbl.NewRow();
                        dr["IdUpload"] = IdFileName;
                        dr["Data"] = dataspliting.ElementAt(1);
                        dr["NumberColumn"] = Column;
                        dr["NumerRow"] = row;
                        dr["DateCreation"] = DateTime.Now;
                        tbl.Rows.Add(dr);
                    }
                }
                else
                {
                    dr["IdUpload"] = IdFileName;
                    dr["Data"] = data;
                    dr["NumberColumn"] = Column;
                    dr["NumerRow"] = row;
                    dr["DateCreation"] = DateTime.Now;
                    tbl.Rows.Add(dr);
                }
                Column++;
            }
            _registersData.SaveRegisters(tbl);       
        }
    }
}
