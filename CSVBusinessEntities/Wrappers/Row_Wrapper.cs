using System;
using System.Collections.Generic;
using System.Text;

namespace CSVBusinessEntities.Wrappers
{
    public class Row_Wrapper
    {
        public int numberRow { get; set; }
        public int numberColumn { get; set; }
        public string data { get; set; }
        public int FileNameId { get; set; }
    }
}
