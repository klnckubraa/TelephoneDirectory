namespace WebApplication1.DbModel
{
    public class Detail : BaseEntity
    {
        public string Email { get; set; }
        public string Number { get; set; }
        public int TypeId { get; set; }
        public int PersonId { get; set; }

    }
}
