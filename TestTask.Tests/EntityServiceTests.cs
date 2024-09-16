using Moq;
using TestTask.Application.Services;
using TestTask.Domain;
using TestTask.Domain.Interfaces;

namespace TestTask.Tests;

public class EntityServiceTests
{
    private readonly Mock<IEntityRepository> _mockRepository;
    private readonly EntityService _entityService;

    public EntityServiceTests()
    {
        _mockRepository = new Mock<IEntityRepository>();
        _entityService = new EntityService(_mockRepository.Object);
    }

    [Fact]
    public void InsertEntity_should_call_repository_add_method()
    {
        // Arrange
        var entityId = Guid.Parse("cfaa0d3f-7fea-4423-9f69-ebff826e2f89");
        var operationDate = new DateTimeOffset(2019, 4, 2, 13, 10, 20, TimeSpan.FromHours(3));
        const decimal AMOUNT = 23.05M;

        var entityJson =
        $$$"""
        {
            "id":"{{{entityId}}}",
            "operationDate":"{{{operationDate.ToString(Constants.DATE_TIME_OFFSET_FROM_GET_REQUEST_FORMAT)}}}",
            "amount":{{{AMOUNT.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}}}
        }
        """;

        // Act
        _entityService.InsertEntity(entityJson);

        // Assert
        _mockRepository.Verify(r => r.Add(It.Is<Entity>(e => e.Id == entityId && e.Amount == AMOUNT)), Times.Once);
    }


    [Fact]
    public void GetEntity_should_return_entity_if_found()
    {
        // Arrange
        const decimal AMOUNT = 200.75M;
        var operationDate = new DateTimeOffset(2020, 4, 2, 13, 10, 20, TimeSpan.FromHours(3));
        var entityId = Guid.Parse("cfaa0d3f-7fea-4423-9f69-ebff826e2f80");

        var entity = new Entity
        {
            Id = entityId,
            OperationDate = operationDate.DateTime,
            Amount = AMOUNT
        };

        _mockRepository.Setup(r => r.GetById(entityId)).Returns(entity);

        // Act
        var result = _entityService.GetEntity(entityId);

        // Assert
        Assert.Equal(entityId, result.Id);
        Assert.Equal(AMOUNT, result.Amount);
    }

    [Fact]
    public void GetEntity_should_throw_exception_if_entity_not_found()
    {
        // Arrange
        var entityId = Guid.Parse("cfaa0d3f-7fea-4423-9f69-ebff826e2f80");

        _mockRepository.Setup(r => r.GetById(entityId)).Returns((Entity)null);

        // Act
        var exception = Assert.Throws<KeyNotFoundException>(() => _entityService.GetEntity(entityId));

        // Assert
        Assert.Equal($"Entity with ID {entityId} not found", exception.Message);
    }
}