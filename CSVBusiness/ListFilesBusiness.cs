using CSVBusiness.Interface;
using CSVBusinessEntities.Wrappers;
using CSVDataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVBusiness
{
    public class ListFilesBusiness : IListFilesBusiness
    {
        private readonly IListFilesDataAccess _listFilesDataAccess;

        public ListFilesBusiness (IListFilesDataAccess ListFilesDataAccess)
        {
            _listFilesDataAccess = ListFilesDataAccess;
        }

        public async Task<ICollection<FileName_Wrapper>> getsFilesNames()
        {
            var FilesNames = await _listFilesDataAccess.GetFilesNames();
            return FilesNames;

        }

    }
}
