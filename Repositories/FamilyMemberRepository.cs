using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using mofa_test.DBContextEFCoreInMemoryDbDemo;
using mofa_test.Models;

namespace mofa_test.Repositories
{
    public interface IFamilyMemberRepository
    {
        Task<bool> Delete(int id);
        Task<FamilyMemberNationalityVM?> FamilyMemberNationality(int id);
        Task<FamilyMember> UpdateFamilyMember(int id, FamilyMember familyMember);
        Task<FamilyMemberNationalityVM?> UpdateFamilyMemberNationality(int id, int nationalityId);
    }

    public class FamilyMemberRepository : IFamilyMemberRepository
    {
        public FamilyMemberRepository()
        {
        }
        public async Task<bool> Delete(int id)
        {
            using (var context = new ApiContext())
            {
                var familyMember = await context.FamilyMembers.FindAsync(id);
                if (familyMember == null)
                {
                    return false;
                }
                _ = context.FamilyMembers.Remove(familyMember);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<FamilyMemberNationalityVM?> FamilyMemberNationality(int id)
        {
            using (var context = new ApiContext())
            {
                var familyMember = await context.FamilyMembers.FindAsync(id);
                if (familyMember == null)
                {
                    return null;
                }
                var fmNationality = await context.FamilyMembersNationalities.SingleOrDefaultAsync(fmn => fmn.FamilyMemberId == familyMember.ID);
                if (fmNationality != null)
                {
                    return new FamilyMemberNationalityVM
                    {
                        ID = familyMember.ID,
                        FirstName = familyMember.FirstName,
                        LastName = familyMember.LastName,
                        NationalityId = fmNationality.NationalityId,
                        RelationshipId = familyMember.RelationshipId
                    };
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<FamilyMember> UpdateFamilyMember(int id, FamilyMember familyMember)
        {
            using (var context = new ApiContext())
            {
                var _familyMember = await context.FamilyMembers.FirstOrDefaultAsync(fm => fm.ID == id);
                if (_familyMember == null)
                    return null;

                _familyMember.FirstName = familyMember.FirstName;
                _familyMember.LastName = familyMember.LastName;
                _familyMember.RelationshipId = familyMember.RelationshipId;
                _familyMember.DateOfBirth = familyMember.DateOfBirth;

                await context.SaveChangesAsync();
                return _familyMember;
            }
        }

        public async Task<FamilyMemberNationalityVM?> UpdateFamilyMemberNationality(int id, int nationalityId)
        {

            using (var context = new ApiContext())
            {
                var familyMember = await context.FamilyMembers.SingleOrDefaultAsync(fm => fm.ID == id);
                if (familyMember == null)
                    return null;

                var familyMemberNationality = await context.FamilyMembersNationalities.SingleOrDefaultAsync(fmn => fmn.FamilyMemberId == id);
                if (familyMemberNationality == null)
                {
                    await context.FamilyMembersNationalities.AddAsync(new FamilyMemberNationality { FamilyMemberId = id, NationalityId = nationalityId });
                }
                else
                {
                    familyMemberNationality.NationalityId = nationalityId;
                }
                await context.SaveChangesAsync();

                return new FamilyMemberNationalityVM
                {
                    ID = familyMember.ID,
                    LastName = familyMember.LastName,
                    FirstName = familyMember.FirstName,
                    NationalityId = nationalityId
                };

            }

        }

    }
}
