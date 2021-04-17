using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using BlazingSodium.Shared;
using BlazingSodium.Server.Persistence.Repositories;

namespace BlazingSodium.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        // The Web API will only accept tokens 1) for users, and 2) having the "API.Access" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "API.Access" };

        private readonly EmployeeRepositoryInterface _employeeRepository;

        public EmployeeController(EmployeeRepositoryInterface employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult CreateEmployee(CreationEmployeeDto employeeDTO)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            Employee employee = _employeeRepository.CreateEmployee(employeeDTO.Name, employeeDTO.Age);

            if (employee != null)
            {
                _employeeRepository.Save();
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTournament(Guid id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            bool employeeRemoved = _employeeRepository.DeleteEmployee(id);

            if (employeeRemoved)
            {
                _employeeRepository.Save();
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            return Ok(_employeeRepository.GetEmployees());
        }

        [HttpGet("{identifier}")]
        public ActionResult<IEnumerable<Employee>> GetEmployee(string identifier)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            if (Guid.TryParse(identifier, out Guid id))
            {
                return Ok(_employeeRepository.GetEmployee(id));
            }

            return Ok(_employeeRepository.GetEmployee(identifier));
        }
    }
}
