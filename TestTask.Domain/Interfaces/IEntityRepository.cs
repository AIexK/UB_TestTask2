namespace TestTask.Domain.Interfaces;

public interface IEntityRepository
{
    void Add(Entity entity);
    Entity GetById(Guid id);
}
