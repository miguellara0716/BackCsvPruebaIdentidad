using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVBusiness.Interface
{
    public interface IListRegistersBusiness
    {
        Task<string> GetRegistersFilerByFile(int IdFile);
    }
}
