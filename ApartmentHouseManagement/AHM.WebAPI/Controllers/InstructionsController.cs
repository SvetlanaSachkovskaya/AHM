using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.Common.Helpers;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
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
            if (AppUser.BuildingId.HasValue)
            {
                instruction.BuildingId = AppUser.BuildingId.Value;
            }
            instruction.CreateDate = DateTime.Now;

            await _instructionService.AddAsync(instruction);

            return Ok(instruction);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Instruction instruction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _instructionService.UpdateAsync(instruction);

            return Ok();
        }
    }
}