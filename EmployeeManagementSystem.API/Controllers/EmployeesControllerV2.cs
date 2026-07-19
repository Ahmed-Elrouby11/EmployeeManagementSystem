using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesControllerV2 : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Message = "This is version 2.0 of the Employees API."
            });
        }
    }
}
