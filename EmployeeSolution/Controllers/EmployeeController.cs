using Dapper;
using EmployeeSolution.Helpers;
using EmployeeSolution.Models;
using EmployeeSolution.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private IEmployeeService _employeeService;

        public EmployeeController(IConfiguration config,IEmployeeService employeeService)
        {
            _config = config;
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployeesList")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesList()
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnectionString"));
                var employees = await connection.QueryAsync<Employee>("GetEmployeesList");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
       

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee info)
        {
            try
            {

                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnectionString"));
                await connection.ExecuteAsync("insert into EmployeeInfo (firstname, lastname, department,username,password) values(@FirstName,@LastName,@DepartMent,@Username,@Password)", info);
                return Ok(await SelectAllEmployees(connection));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee info)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnectionString"));
            await connection.ExecuteAsync("update EmployeeInfo set firstname=@FirstName,lastname=@LastName,department=@DepartMent,username=@Username,password=@Password where employeeidpk=@EmployeeIDPK", info);
            return Ok(await SelectAllEmployees(connection));

        }

        [HttpDelete("{employeeid}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int employeeid)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnectionString"));
            await connection.ExecuteAsync("delete from EmployeeInfo where EmployeeIDPK=@EmployeeIDPK", new {EmployeeIDPK= employeeid});
            return Ok(await SelectAllEmployees(connection));

        }
        private static async Task<IEnumerable<Employee>> SelectAllEmployees(SqlConnection connection)
        {
            return await connection.QueryAsync<Employee>("select * from EmployeeInfo");
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _employeeService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var users = _employeeService.GetAll();
            return Ok(users);
        }

    }
}
