using FluentAssertions;
using Moq;
using ProjetoTopdown.Application.Interfaces;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Application.OrderFunctions.Commands.CreateOrder;
using ProjetoTopdown.Application.OrderFunctions.Dtos;
using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Tests.Application.UnitTests.OrderFunctions.Commands;
#pragma warning disable CA1707 // Identificadores não devem conter sublinhados
#pragma warning disable CA2007 // Considere chamar ConfigureAwait na tarefa esperada
public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateOrderCommandHandler(
            _productRepositoryMock.Object,
            _customerRepositoryMock.Object,
            _orderRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }


    [Fact]
    public async Task Handle_WhenOrderIsValid_ShouldCreateOrderAndReturnId()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            CustomerId = 1,
            Items = new List<OrderItemDto> { new() { ProductId = 1, Quantity = 2 } }
        };

        var customer = new Customer("Teste", "teste@teste.com", "123");
        var product = new Product("Produto Teste", "SKU-01", 100, 10);
        typeof(Product).GetProperty("Id")?.SetValue(product, 1);

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _productRepositoryMock.Setup(r => r.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product> { product });

        _orderRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
            .Callback<Order, CancellationToken>((order, ct) =>
            {

                typeof(Order).GetProperty("Id")?.SetValue(order, 123);
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        product.StockQty.Should().Be(8);
        _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        result.Should().Be(123);
    }

    [Fact]
    public async Task Handle_WhenStockIsInsufficient_ShouldThrowValidationException()
    {
        // Arrange 
        var command = new CreateOrderCommand
        {
            CustomerId = 1,
            Items = new List<OrderItemDto> { new() { ProductId = 1, Quantity = 10 } }
        };

        var customer = new Customer("Teste", "teste@teste.com", "123");
        var product = new Product("Produto Teste", "SKU-01", 100, 5);

        typeof(Product).GetProperty("Id")?.SetValue(product, 1);

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _productRepositoryMock.Setup(r => r.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product> { product });

        // Act 
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert 
        await act.Should().ThrowAsync<ProjetoTopdown.Application.Exceptions.ValidationException>()
            .WithMessage("*Estoque insuficiente*");

        _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenIdempotencyKeyAlreadyExists_ShouldReturnExistingOrderIdAndNotCreateNewOrder()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            IdempotencyKey = Guid.NewGuid(),
            CustomerId = 1,
            Items = new List<OrderItemDto> { new() { ProductId = 1, Quantity = 2 } }
        };

        var existingOrder = new Order(command.CustomerId, command.IdempotencyKey);
        typeof(Order).GetProperty("Id")?.SetValue(existingOrder, 99);

        _orderRepositoryMock
            .Setup(r => r.GetByIdempotencyKeyAsync(command.IdempotencyKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingOrder);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert 
        result.Should().Be(99);

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        _productRepositoryMock.Verify(r => r.GetByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()), Times.Never);
        _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}