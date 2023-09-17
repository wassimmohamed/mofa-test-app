using Microsoft.EntityFrameworkCore;
using mofa_test.DBContextEFCoreInMemoryDbDemo;
using mofa_test.Models;

namespace mofa_test.Repositories
{
    public interface ILookupRepository
    {
        Task<IEnumerable<Nationality>> GetAllNationalities();

        Task<IEnumerable<Relationship>> GetAllRelationships();
    }

    public class LookupRepository : ILookupRepository
    {
        public LookupRepository()
        {
            try
            {


                using (var context = new ApiContext())
                {
                    if (!context.Nationalities.Any())
                    {

                        var countries = new List<Nationality> {
                new Nationality{
                    ID= 1,
                    Country = "UAE",
                },
                new Nationality{
                    ID= 2,
                    Country = "India",
                },
                new Nationality{
                    ID= 3,
                    Country = "Pakistan",
                },
                new Nationality{
                    ID= 4,
                    Country = "France",
                }
                };
                        context.Nationalities.AddRange(countries);
                        context.SaveChanges();
                    }

                    if (!context.Relationships.Any())
                    {
                        var relations = new List<Relationship> {
                        new Relationship{
                            ID= 1,
                            Title = "Parent",
                            },
                        new Relationship{
                            ID= 2,
                            Title= "Sibling",
                            },
                        new Relationship{
                            ID= 3,
                            Title= "Spouse",
                            },
                        };
                        context.Relationships.AddRange(relations);
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
        public async Task<IEnumerable<Nationality>> GetAllNationalities()
        {
            using (var context = new ApiContext())
            {
                return await context.Nationalities.ToListAsync();
            }
        }
        public async Task<IEnumerable<Relationship>> GetAllRelationships()
        {
            using (var context = new ApiContext())
            {
                return await context.Relationships.ToListAsync();
            }
        }
    }
}
