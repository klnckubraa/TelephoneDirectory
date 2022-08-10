using WebApplication1.DbModel;

namespace WebApplication1.Repositories.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(int id);
    }
}
