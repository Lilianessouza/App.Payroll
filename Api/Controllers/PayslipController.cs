using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayslipController : ControllerBase
    {
        private readonly IPayslipService _service;
        public PayslipController(IPayslipService contrachequeService)
        {
            _service = contrachequeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayslipDto>>> GetAllPayslip()
        {
            var result = await _service.GetAllPayslipEmployee();
            if (result.IsSucess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("{id:int}", Name = "GetPayslipById")]
        public async Task<ActionResult<PayslipDto>> GetPayslipByIdEmployee(int id)
        {
            var result = await _service.GetPayslipByEmployeeId(id);
            if (result.IsSucess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
