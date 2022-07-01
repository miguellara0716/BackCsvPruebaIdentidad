using CSVBusiness.Interface;
using CSVBusinessEntities.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly IValidateCredentialsBusiness _validateCredentials;
        private readonly ISaveFileBusiness _savedFileBusiness;
        private readonly IListFilesBusiness _listFilesBusiness;
        private readonly IListRegistersBusiness _listRegistersBusiness;

        public CSVController(IValidateCredentialsBusiness ValidateCredentials, ISaveFileBusiness SavedFileBusiness,
                              IListFilesBusiness ListFilesBusiness, IListRegistersBusiness ListRegistersBusiness)
        {
            _validateCredentials = ValidateCredentials;
            _savedFileBusiness = SavedFileBusiness;
            _listFilesBusiness = ListFilesBusiness;
            _listRegistersBusiness = ListRegistersBusiness;
        }

        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> Save([FromBody] SaveItem_Wrapper DataFile)
        {
            try
            {
                string authHeader = HttpContext.Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader) || !_validateCredentials.Validate(authHeader).Result)
                {
                    return Unauthorized();
                }

                _savedFileBusiness.SavedFile(DataFile);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetFilesNames")]
        public async Task<ActionResult> GetFilesNames()
        {
            try
            {
                string authHeader = HttpContext.Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader) || !_validateCredentials.Validate(authHeader).Result)
                {
                    return Unauthorized();
                }
                var Result = await _listFilesBusiness.getsFilesNames();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetRegister/{IdFile}")]
        public async Task<ActionResult> GetFilesNames(int IdFile)
        {
            try
            {
                string authHeader = HttpContext.Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader) || !_validateCredentials.Validate(authHeader).Result)
                {
                    return Unauthorized();
                }
                var Result = await _listRegistersBusiness.GetRegistersFilerByFile(IdFile);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
