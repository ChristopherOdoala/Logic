using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data;
using Logic.Model;
using Logic.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logic.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatusController : BaseApiController
    {
        private readonly IStatusService _statusService;
        public StatusController(IRepository repo, IStatusService statusService)
            :base(repo)
        {
            this._statusService = statusService;
        }

        [HttpPost]
        [Route("{employeeId}")]
        public IActionResult CreateStatus(Guid employeeId)
        {
            var result = _statusService.CreateInitialStatus(employeeId);
            if(result.Count > 0)
                return Ok(ApiResponse<string>(message: string.Join(";", result), codes: ApiResponseCodes.ERROR));
            return Ok();
        }
    }
}