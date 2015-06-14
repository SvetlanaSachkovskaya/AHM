using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.Common.Helpers;
using AHM.WebAPI.Attributes;

namespace AHM.WebAPI.Controllers
{
    [Authorization(Roles = new[] { Roles.Concierge, Roles.Worker })]
    [RoutePrefix("api/Instructions")]
    public class InstructionsController : BaseController
    {
        private readonly IInstructionsService _instructionService;


        public InstructionsController(IInstructionsService instructionService)
        {
            _instructionService = instructionService;
        }


        [HttpGet]
        [Route("GetAllInstructionsByDate")]
        public async Task<IHttpActionResult> GetAllInstructionsByDate(DateTime date)
        {
            var instructions = await _instructionService.GetInstructionsByDateAsync(AppUser.BuildingId ?? 0, date, false);
            return Ok(instructions.OrderByDescending(i => i.Priority));
        }

        [HttpGet]
        [Route("GetAllOpenInstructionsByDate")]
        public async Task<IHttpActionResult> GetAllOpenInstructionsByDate(DateTime date)
        {
            var instructions = await _instructionService.GetInstructionsByDateAsync(AppUser.BuildingId ?? 0, date, true);
            return Ok(instructions.OrderByDescending(i => i.Priority));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var instruction = await _instructionService.GetByIdAsync(id);
            return Ok(instruction);
        }

        [HttpGet]
        [Route("GetPriorities")]
        public OkNegotiatedContentResult<EnumCollection<Priority>> GetPriorities()
        {
            var priotities = new EnumCollection<Priority>();
            return Ok(priotities);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(Instruction instruction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            instruction.EmploeeId = AppUser.Id;
            if (AppUser.BuildingId.HasValue)
            {
                instruction.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _instructionService.AddAsync(instruction);

            return result.IsSuccessful ? (IHttpActionResult) Ok(instruction) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Instruction instruction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _instructionService.UpdateAsync(instruction);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Instruction instruction)
        {
            var result = await _instructionService.RemoveAsync(instruction);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
    }
}