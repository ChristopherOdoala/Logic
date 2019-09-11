using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data;
using Logic.Model.ViewModel;
using Logic.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logic.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : BaseApiController
    {
        private IStatusService _statusService;
        public EmployeesController(IRepository repo, IStatusService statusService)
            :base(repo)
        {
            this._statusService = statusService;
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] EmployeeViewModel model)
        {
            var employee = new Employee
            {
                Name = model.Name,
                Address = model.Address
            };
            try
            {
                if (TheRepository.Insert(employee) && TheRepository.SaveAll())
                {
                    return Ok();
                }
            }
            catch(Exception ex)
            {
                HandleError(ex);
            }
            

            return BadRequest();
        }
    }
}