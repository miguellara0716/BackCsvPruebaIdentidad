using CSVBusiness.Interface;
using CSVBusinessEntities.Wrappers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSVBusiness
{
    public class ValidateCredentialsBusiness : IValidateCredentialsBusiness
    {
        private readonly IConfiguration _configuration;

        public ValidateCredentialsBusiness (IConfiguration Configuration)
        {
            _configuration = Configuration;
        }           

        public async Task<bool> Validate (string Credentials)
        {
            string UrlSecurity = _configuration.GetValue<string>("UrlSecurity");
            var client = new RestClient(UrlSecurity);
            var request = new RestRequest("api/Authentication/Login");
            request.AddHeader("Authorization", Credentials);
            var response = await client.GetAsync(request);
            var Result = JsonConvert.DeserializeObject<ResultAutentication_Wrapper>(response.Content);
            return Result.IsSuccess;
        }
    }
}
