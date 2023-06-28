using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContraCheque.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService EmployeeService)
    {
        _employeeService = EmployeeService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployee()
    {
        var result = await _employeeService.GetAll();
        if (result.IsSucess)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpGet("{id:int}", Name = "GetEmployee")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var result = await _employeeService.GetById(id);

        if (result.StatusCode == HttpStatusCode.NoContent) return NoContent();

        if (result.IsSucess)
            return Ok(result);
        else
            return BadRequest(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> Insert([FromBody] EmployeeDto employeeDto)
    {
        var result = await _employeeService.Insert(employeeDto);

        if (result.IsSucess)
            return Created("Insert", result);
        else
            return BadRequest(result);

    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Update([FromBody] EmployeeDto employeeDto)
    {
        var result = await _employeeService.Update(employeeDto);
        if (result.IsSucess)
            return new CreatedAtRouteResult("GetEmployee", new { id = employeeDto.Id }, employeeDto);
        else
            return BadRequest();
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> Delete(int id)
    {
        await _employeeService.Delete(id);
        return Ok();
    }

}
