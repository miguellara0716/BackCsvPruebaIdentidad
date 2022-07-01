using CSVBusinessEntities.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataAccess.Interface
{
    public interface IListRegistersDataAccess
    {
        Task<ICollection<Registers_Wrappers>> GetRegistersFilerByFile(int IdFile);
    }
}
