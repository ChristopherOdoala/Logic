using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data;
using Logic.Model;
using Logic.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Logic.Model.Status;

namespace Logic.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThirdPartyValidationController : BaseApiController
    {
        private IStatusService _statusService;
        public ThirdPartyValidationController(IRepository repo, IStatusService statusService)
            :base(repo)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult GetThirdPartyTrucks()
        {
            var query = (from e in TheRepository.GetAllEmployee()
                         join s in TheRepository.GetAllStatus()
                         on e.Id equals s.EmployeeId
                         where e.Status == false && (int)s.CurrentStage == (int)ValidationLevel.Third_Party_Validation
                         select new
                         {
                             Name = e.Name,
                             Address = e.Address
                         });
            var result =  query.ToList();
            return Ok(new ApiResponse<Object>
            {   
                Code = ApiResponseCodes.OK,
                Payload = result
            });
        }

        [HttpPost]
        public IActionResult Validate(Guid employeeId)
        {
            try
            {
                if (employeeId == null)
                    return BadRequest("Employee Id is null");
                var employeeStatus = TheRepository.GetAllStatus().Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                if (employeeStatus == null)
                    return BadRequest("Employee Status is null");
                var statusResult = _statusService.UpdateToNextStatus(employeeStatus);
                if (statusResult.Item1)
                {
                    return Ok("Employee has been validated by you and moved for next level validation");
                }

                if (statusResult.Item2)
                {
                    var employee = TheRepository.GetAllEmployee().Where(x => x.Id == employeeId).FirstOrDefault();
                    employee.Status = true;
                    if (employee == null)
                        return BadRequest("Employee Record is null");
                    if(TheRepository.Update(employee) && TheRepository.SaveAll())
                    {
                        return Ok("Employee has been completely validated");
                    }
                }

                if(statusResult.Item3.Count() > 0)
                {
                    return BadRequest(statusResult.Item3);
                }

            }
            catch(Exception ex)
            {
                HandleError(ex);
            }

            return BadRequest("Something went wrong");
        }
    }
}