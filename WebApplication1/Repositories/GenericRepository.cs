using Microsoft.EntityFrameworkCore;
using WebApplication1.DbModel;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)

        {
            _context = context;
            _logger = logger;

        }

        public bool Update(TEntity entity)
        {
            try
            {
                entity.Updated_date = DateTime.UtcNow;
                var data = _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == entity.Id);
                _context.Set<TEntity>().Update(entity);
                entity.Created_date = data.Created_date;
                entity.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine("Güncelleme Başarılı değil", ex.Message);
                return false;
            }
        }

        public virtual bool Create(TEntity entity)
        {
            try
            {
                entity.Created_date = DateTime.UtcNow;
                entity.Updated_date = DateTime.UtcNow;
                _context.Add<TEntity>(entity);
                entity.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine("Başarılı değil", ex.Message);
                var error = ex.Message;
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                entity.IsDeleted = false;
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine("Silme Başarılı değil", ex.Message);
                return false;
            }
        }

        public List<TEntity> GetAll()
        {

            return _context.Set<TEntity>().Where(x => x.IsDeleted).ToList();
        }

        public TEntity GetById(int id)
        {
            var data = _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                _logger.LogError("Böyle bir veri yok");
                return null;
            }
            else if (data.IsDeleted == true)
            {
                return data;
            }
            else
            {
                _logger.LogInformation("Bu data silinmiş");
                return null;
            }
        }
    }
}
