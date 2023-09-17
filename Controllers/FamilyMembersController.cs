using Microsoft.AspNetCore.Mvc;
using mofa_test.Models;
using mofa_test.Repositories;

namespace mofa_test.Controllers
{
    [ApiController]
    [Route("api")]
    public class FamilyMembersController : ControllerBase
    {
        
        private readonly ILogger<FamilyMembersController> _logger;
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public FamilyMembersController(ILogger<FamilyMembersController> logger, IFamilyMemberRepository familyMemberRepository)
        {
            _logger = logger;
            _familyMemberRepository = familyMemberRepository;
        }


        [HttpPut]
        [Route("FamilyMembers/{id}")]
        public async Task<ActionResult<bool>> UpdateFamilyMember(int id, FamilyMember familyMember)
        {
            return Ok(await _familyMemberRepository.UpdateFamilyMember(id, familyMember));
        }

        [HttpDelete]
        [Route("FamilyMembers/{id}")]
        public async Task<ActionResult<bool>> DeleteFamilyMember(int id)
        {
            return Ok(await _familyMemberRepository.Delete(id));
        }
        [HttpGet]
        [Route("FamilyMembers/{id}/Nationality")]
        public async Task<ActionResult<FamilyMemberNationalityVM>> FamilyMemberNationality(int id)
        {
            return Ok(await _familyMemberRepository.FamilyMemberNationality(id));
        }

        [HttpPut]
        [Route("FamilyMembers/{id}/Nationality/{nationalityId}")]
        public async Task<ActionResult<FamilyMemberNationalityVM>> UpdateFamilyMemberNationality(int id, int nationalityId)
        {
            return Ok(await _familyMemberRepository.UpdateFamilyMemberNationality(id, nationalityId));
        }
    }
}