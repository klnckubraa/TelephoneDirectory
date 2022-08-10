using WebApplication1.DbModel;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Repositories
{
    public class TypeRepository : GenericRepository<TypeEntity>, ITypeRepository
    {
        public TypeRepository(ApplicationDbContext dbcontext, ILogger<DetailRepository> logger) : base(dbcontext, logger)
        {

        }

    }
}
