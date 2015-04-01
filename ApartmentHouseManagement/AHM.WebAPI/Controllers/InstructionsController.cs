using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.Common.Helpers;

namespace AHM.WebAPI.Controllers
{
    [Authorize(Roles = "Concierge,Worker")]
    [RoutePrefix("api/Instructions")]
    public class InstructionsController : BaseController
    {
        private readonly IInstructionsService _instructionService;


        public InstructionsController(IInstructionsService instructionService)
        {
            _instructionService = instructionService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var instructions = await _instructionService.GetAllInstructionsAsync(AppUser.BuildingId ?? 0);
            return Ok(instructions);
        }

        [HttpGet]
        [Route("GetAllOpen")]
        public async Task<IHttpActionResult> GetAllOpen()
        {
            var instructions = await _instructionService.GetAllOpenInstructionsAsync(AppUser.BuildingId ?? 0);
            return Ok(instructions);
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
                return BadRequest(ModelState);
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
                return BadRequest(ModelState);
            }

            var result = await _instructionService.UpdateAsync(instruction);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
    }
}