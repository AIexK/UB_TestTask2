using TestTask.Domain;
using TestTask.Domain.Interfaces;

namespace TestTask.Infrastructure.Services;

public class InMemoryEntityRepository : IEntityRepository
{
    private readonly Dictionary<Guid, Entity> _entities = new();

    public void Add(Entity entity)
    {
        _entities[entity.Id] = entity;
    }

    public Entity GetById(Guid id)
    {
        return _entities.TryGetValue(id, out var entity) ? entity : null;
    }
}