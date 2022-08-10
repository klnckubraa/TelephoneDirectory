using WebApplication1.DbModel;

namespace WebApplication1.Repositories.Interface
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        bool PersonControl(string name, string surname);

    }
}
