using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVBusiness.Interface
{
    public interface IValidateCredentialsBusiness
    {
        Task<bool> Validate(string Credentials);
    }
}
