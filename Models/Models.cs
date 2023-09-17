using System.ComponentModel.DataAnnotations;

namespace mofa_test.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }

    public class StudentNationality
    {
        [Key]
        public int ID { get; set; }
        public int StudentId { get; set; }
        public int NationalityId { get; set; }
    }

    public class StudentNationalityVM
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int NationalityId { get; set; }
    }

    public class FamilyMemberNationalityVM
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int NationalityId { get; set; }
        public int RelationshipId { get; set; }
    }
    public class FamilyMember
    {
        [Key]
        public int ID { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int RelationshipId { get; set; }
    }
    public class FamilyMemberVM: FamilyMember
    {
        public int? NationalityId { get; set; }
    }
    public class FamilyMemberNationality
    {
        [Key]
        public int ID { get; set; }
        public int FamilyMemberId { get; set; }
        public int NationalityId { get; set; }
    }
    public class Nationality
    {
        [Key]
        public int ID { get; set; }
        public string Country { get; set; } = string.Empty;

    }
    public class Relationship
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;

    }
}
