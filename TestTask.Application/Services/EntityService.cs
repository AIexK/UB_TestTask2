using System.Text.Json;
using TestTask.Application.Common.Interfaces;
using TestTask.Application.Models.RequestModels;
using TestTask.Domain;
using TestTask.Domain.Builders;
using TestTask.Domain.Interfaces;

namespace TestTask.Application.Services;

public class EntityService : IEntityService
{
    private readonly IEntityRepository _repository;

    public EntityService(IEntityRepository repository)
    {
        _repository = repository;
    }

    public void InsertEntity(string insertCommmandJson)
    {
        var entityRequest = JsonSerializer
            .Deserialize<InsertRequest>(insertCommmandJson);

        _repository.Add(new EntityBuilder()
            .SetId(entityRequest.Id)
            .SetOperationDate(entityRequest.OperationDate)
            .SetAmount(entityRequest.Amount)
            .Build());
    }

    public Entity GetEntity(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found");
        }
        return entity;
    }
}