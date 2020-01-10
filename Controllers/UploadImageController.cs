using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ex_upload_image_azure_storage.Models;
using ex_upload_image_azure_storage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ex_upload_image_azure_storage.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
       
       [HttpPost]
       public async Task<ActionResult> Post([FromBody]Image entity)
       {
           try
            {
                if (entity == null)
                {
                    return BadRequest("Client object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var result = await FileUpload.UploadBase64Image(entity.Base64Image);

                return StatusCode(200, result);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
       }
      
    }
}