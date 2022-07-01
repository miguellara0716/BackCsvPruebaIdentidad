using CSVBusinessEntities.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataAccess.Interface
{
    public interface IListFilesDataAccess
    {
        Task<ICollection<FileName_Wrapper>> GetFilesNames();
    }
}
