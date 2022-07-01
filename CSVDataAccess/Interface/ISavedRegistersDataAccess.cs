using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CSVDataAccess.Interface
{
    public interface ISavedRegistersDataAccess
    {
        void SaveRegisters(DataTable Table);
    }
}
