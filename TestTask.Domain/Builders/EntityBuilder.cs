namespace TestTask.Domain.Builders;

public class EntityBuilder
{
    private Entity _entity = new();

    
    public EntityBuilder SetId(string id)
    {
        var wasIdParsed = Guid.TryParse(id, out var parsedId);

        if (!wasIdParsed)
        {
            throw new ArgumentException($"Wrong ID format: {id}");
        }

        _entity.Id = parsedId;
        return this;
    }

    public EntityBuilder SetOperationDate(DateTimeOffset operationDate)
    {
        _entity.OperationDate = operationDate.DateTime;
        return this;
    }

    public EntityBuilder SetAmount(float amount)
    {
        _entity.Amount = Convert.ToDecimal(amount);
        return this;
    }

    public Entity Build() => _entity;
}
