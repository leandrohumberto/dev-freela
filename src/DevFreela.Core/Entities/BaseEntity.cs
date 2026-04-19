namespace DevFreela.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Deleted = false;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Deleted { get; private set; }

        public virtual void Delete()
        {
            Deleted = true;
        }
    }
}
