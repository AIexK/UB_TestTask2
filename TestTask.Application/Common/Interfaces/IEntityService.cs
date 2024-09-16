using TestTask.Domain;

namespace TestTask.Application.Common.Interfaces;

public interface IEntityService
{
    void InsertEntity(string insertCommmandJson);
    Entity GetEntity(Guid id);
}