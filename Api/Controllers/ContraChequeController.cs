using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContraChequeController : ControllerBase
    {
        private readonly IContraChequeService _contrachequeService;
        public ContraChequeController(IContraChequeService contrachequeService)
        {
            _contrachequeService = contrachequeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployee()
        {
            var result = await _contrachequeService.GetAllFuncionario();
            if (result.IsSucess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("{id:int}", Name = "GetContraChequeById")]
        public async Task<ActionResult<EmployeeDto>> GetContraChequeById(int id)
        {
            var result = await _contrachequeService.GetContraChequeByFuncionarioId(id);
            if (result.IsSucess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
