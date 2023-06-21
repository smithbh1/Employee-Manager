using crud.Data;
using crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly crudDbContent _crudDbContent;


        public EmployeesController(crudDbContent crudDbContent)
        {
            _crudDbContent = crudDbContent;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _crudDbContent.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            employeeRequest.CreatedDate = DateTime.Now;
            await _crudDbContent.Employees.AddAsync(employeeRequest);
            await _crudDbContent.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _crudDbContent.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _crudDbContent.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _crudDbContent.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteEmployee([FromRoute] Guid id)
        {
            var employee = await _crudDbContent.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            _crudDbContent.Employees.Remove(employee);
            await _crudDbContent.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
