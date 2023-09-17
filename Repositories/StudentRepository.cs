using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using mofa_test.DBContextEFCoreInMemoryDbDemo;
using mofa_test.Models;

namespace mofa_test.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> Add(Student student);
        Task<Student?> Get(int id);
        Task<IEnumerable<Student>> GetAll();
        Task<Student?> Update(int id, Student studentModel);
        Task<StudentNationalityVM?> GetStudentNationality(int id);
        Task<StudentNationalityVM> UpdateStudentNationality(int id, int nationalityId);
        Task<List<FamilyMemberVM>> GetStudentFamilyMembers(int id);
        Task<FamilyMember> AddFamilyMember(int id, FamilyMember familyMember);
    }

    public class StudentRepository : IStudentRepository
    {
        public StudentRepository()
        {
            try
            {


                using (var context = new ApiContext())
                {
                    if (!context.Students.Any())
                    {

                        var students = new List<Student> {
                new Student{
                    ID= 1,
                    DateOfBirth= new DateTime(1989,4,18),
                    FirstName="Wassim Mohamed",
                    LastName     = "Salahudeen",
                },
                new Student{
                    ID= 2,
                    DateOfBirth= new DateTime(1990,4,18),
                    FirstName="Riyasath",
                    LastName     = "Salahudeen",
                },
                };
                        context.Students.AddRange(students);
                        context.SaveChanges();
                    }
                    if (!context.FamilyMembers.Any())
                    {

                        var familyMembers = new List<FamilyMember> {
                new FamilyMember{
                    ID= 1,
                    DateOfBirth= new DateTime(1960,4,18),
                    FirstName="Salahudeen",
                    LastName     = "Abdul",
                    StudentId= 1,
                    RelationshipId= 1,
                },
                new FamilyMember{
                    ID= 2,
                    DateOfBirth= new DateTime(1978,4,18),
                    FirstName="Shamim",
                    LastName     = "Salahudeen",
                    StudentId= 1,
                    RelationshipId= 3,
                },
                };
                        context.FamilyMembers.AddRange(familyMembers);
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
            }

        }
        public async Task<IEnumerable<Student>> GetAll()
        {
            using (var context = new ApiContext())
            {
                return await context.Students.ToListAsync();
            }
        }
        public async Task<Student?> Get(int id)
        {
            using (var context = new ApiContext())
            {
                return await context.Students.FindAsync(id);
            }
        }
        public async Task<Student> Add(Student student)
        {
            using (var context = new ApiContext())
            {
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();
                return student;
            }
        }
        public async Task<Student?> Update(int id, Student studentModel)
        {
            using (var context = new ApiContext())
            {
                var student = await context.Students.FindAsync(id);
                if (student != null)
                {
                    student.DateOfBirth = studentModel.DateOfBirth;
                    student.FirstName = studentModel.FirstName;
                    student.LastName = studentModel.LastName;
                    await context.SaveChangesAsync();
                    return student;
                }
                return null;
            }
        }
        public async Task<StudentNationalityVM?> GetStudentNationality(int id)
        {
            var student = await Get(id);
            if (student == null)
                return null;
            using (var context = new ApiContext())
            {

                var studentNationality = await context.StudentNationality.SingleOrDefaultAsync(sn => sn.StudentId == id);
                if (studentNationality == null)
                    return null;

                return new StudentNationalityVM
                {
                    ID = student.ID,
                    LastName = student.LastName,
                    FirstName = student.FirstName,
                    NationalityId = studentNationality.NationalityId
                };

            }
        }

        public async Task<StudentNationalityVM?> UpdateStudentNationality(int id, int nationalityId)
        {
            var student = await Get(id);
            if (student == null)
                return null;
            using (var context = new ApiContext())
            {

                var studentNationality = await context.StudentNationality.SingleOrDefaultAsync(sn => sn.StudentId == id);
                if (studentNationality == null)
                {
                    await context.StudentNationality.AddAsync(new StudentNationality { StudentId = id, NationalityId = nationalityId });
                }
                else
                {
                    studentNationality.NationalityId = nationalityId;
                }
                await context.SaveChangesAsync();

                return new StudentNationalityVM
                {
                    ID = student.ID,
                    LastName = student.LastName,
                    FirstName = student.FirstName,
                    NationalityId = nationalityId
                };

            }

        }
        public async Task<List<FamilyMemberVM>> GetStudentFamilyMembers(int id)
        {
            using (var context = new ApiContext())
            {
                var familyMembers = await context.FamilyMembers.Where(fm => fm.StudentId == id).ToListAsync();
                var familymemberNationalites = await context.FamilyMembersNationalities.Where(fm => familyMembers.Select(s => s.StudentId).Contains(id)).ToListAsync();
                var result = new List<FamilyMemberVM>();
                foreach (var fm in familyMembers)
                {
                    result.Add(new FamilyMemberVM
                    {
                        StudentId = fm.StudentId,
                        DateOfBirth = fm.DateOfBirth,
                        FirstName = fm.FirstName,
                        LastName = fm.LastName,
                        ID = fm.ID,
                        NationalityId = familymemberNationalites.FirstOrDefault(f => f.FamilyMemberId == fm.ID)?.NationalityId ?? 0,
                        RelationshipId = fm.RelationshipId,
                    });
                }
                return result;


            }
        }
        public async Task<FamilyMember> AddFamilyMember(int id, FamilyMember familyMember)
        {
            using (var context = new ApiContext())
            {
                familyMember.StudentId = id;
                await context.FamilyMembers.AddAsync(familyMember);
                await context.SaveChangesAsync();
                return familyMember;
            }
        }
    }
}
