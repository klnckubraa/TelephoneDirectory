using WebApplication1.DbModel;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {

        public PersonRepository(ApplicationDbContext dbcontext, ILogger<PersonRepository> logger) : base(dbcontext, logger)
        {

        }

        public bool PersonControl(string name, string surname)
        {
            return _context.Persons.Any(x => x.Name == name && x.Surname == surname);
        }


        public override bool Create(Person entity)
        {
            try
            {
                if (!PersonControl(entity.Name, entity.Surname))
                {
                    base.Create(entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Başarılı değil", ex.Message);
                return false;
            }
        }
    }
}
