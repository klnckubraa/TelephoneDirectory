namespace WebApplication1.DbModel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Updated_date { get; set; }
        public bool IsDeleted { get; set; }

    }
}
