using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.Common.Helpers;
using AHM.WebAPI.Attributes;

namespace AHM.WebAPI.Controllers
{
    [Authorization(Roles = new[] { Roles.Accountant })]
    [RoutePrefix("api/UtilitiesClause")]
    public class UtilitiesClauseController : BaseController
    {
        private readonly IUtilitiesClauseService _utilitiesClauseService;


        public UtilitiesClauseController(IUtilitiesClauseService utilitiesClauseService)
        {
            _utilitiesClauseService = utilitiesClauseService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var utilitiesClauses =
                await
                    _utilitiesClauseService.GetAllUtilitiesClausesAsync(AppUser.BuildingId ?? 0);

            return Ok(utilitiesClauses);
        }

        [HttpGet]
        [Route("GetActive")]
        public async Task<IHttpActionResult> GetActive()
        {
            var utilitiesClauses =
                await
                    _utilitiesClauseService.GetActiveUtilitiesClausesAsync(AppUser.BuildingId ?? 0);

            return Ok(utilitiesClauses);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var utilitiesClause = await _utilitiesClauseService.GetByIdAsync(id);

            return Ok(utilitiesClause);
        }

        [HttpGet]
        [Route("GetCalculationTypes")]
        public async Task<IHttpActionResult> GetCalculationTypes()
        {
            var calculationTypes = new EnumCollection<CalculationType>();
            return Ok(calculationTypes);
        }

        [HttpGet]
        [Route("GetUtilitiesClauseTypes")]
        public async Task<IHttpActionResult> GetUtilitiesClauseTypes()
        {
            var utilitiesClauseTypes = new EnumCollection<UtilitiesClauseType>();
            return Ok(utilitiesClauseTypes);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(UtilitiesClause utilitiesClause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }
            if (AppUser.BuildingId.HasValue)
            {
                utilitiesClause.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _utilitiesClauseService.AddAsync(utilitiesClause);

            return result.IsSuccessful ? (IHttpActionResult) Ok(utilitiesClause) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(UtilitiesClause utilitiesClause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _utilitiesClauseService.UpdateAsync(utilitiesClause);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
    }
}