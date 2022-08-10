using WebApplication1.DbModel;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Repositories
{
    public class DetailRepository : GenericRepository<Detail>, IDetailRepository
    {
        public DetailRepository(ApplicationDbContext dbcontext, ILogger<DetailRepository> logger) : base(dbcontext, logger)
        {

        }

    }
}
