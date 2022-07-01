using CSVBusinessEntities.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVBusiness.Interface
{
    public interface IListFilesBusiness
    {
        Task<ICollection<FileName_Wrapper>> getsFilesNames();
        
    }
}
