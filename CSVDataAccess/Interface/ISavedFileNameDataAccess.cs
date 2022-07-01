using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataAccess.Interface
{
    public interface ISavedFileNameDataAccess
    {
        Task<int> SavedFileName(string FileName);
    }
}
