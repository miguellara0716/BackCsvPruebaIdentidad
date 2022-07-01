using CSVBusiness.Interface;
using CSVDataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVBusiness
{
    public class ListRegistersBusiness : IListRegistersBusiness
    {
        private readonly IListRegistersDataAccess _listRegistersDataAccess;

        public ListRegistersBusiness (IListRegistersDataAccess ListRegistersDataAccess)
        {
            _listRegistersDataAccess = ListRegistersDataAccess;
        }

        public async Task<string> GetRegistersFilerByFile(int IdFile)
        {
            var Registers = await _listRegistersDataAccess.GetRegistersFilerByFile(IdFile);
            string DataCSV = null;
            int BeforeRow = 0;
            foreach(var Register in Registers)
            {
                if(Register.NumerRow > BeforeRow)
                {
                    DataCSV = String.Concat(DataCSV, "\n",Register.Data);
                    BeforeRow = Register.NumerRow;
                }
                if (!String.IsNullOrEmpty(DataCSV))
                {
                    DataCSV = String.Concat(DataCSV, ",");
                }
                DataCSV = String.Concat(DataCSV, Register.Data);
            }
            return DataCSV;
        }
    }
}
