using Microsoft.AspNetCore.Mvc;
using mofa_test.Models;
using mofa_test.Repositories;

namespace mofa_test.Controllers
{
    [ApiController]
    [Route("api")]
    public class LookupController : ControllerBase
    {
        
        private readonly ILogger<LookupController> _logger;
        private readonly ILookupRepository _lookupRepository;

        public LookupController(ILogger<LookupController> logger, ILookupRepository lookupRepository)
        {
            _logger = logger;
            _lookupRepository = lookupRepository;
        }

        
        [HttpGet]
        [Route("Lookup/Nationalities")]
        public async Task<ActionResult<IEnumerable<Nationality>>> GetAllNationalities()
        {
            return Ok(await _lookupRepository.GetAllNationalities());
        }
        [HttpGet]
        [Route("Lookup/Relationships")]
        public async Task<ActionResult<IEnumerable<Nationality>>> GetAllRelationships()
        {
            return Ok(await _lookupRepository.GetAllRelationships());
        }
    }
}