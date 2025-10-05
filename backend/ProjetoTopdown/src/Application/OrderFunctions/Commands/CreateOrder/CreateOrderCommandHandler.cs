using MediatR;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Interfaces;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTopdown.Application.OrderFunctions.Commands.CreateOrder;

#pragma warning disable MEN003 // Method is too long
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var existingOrder = await _orderRepository.GetByIdempotencyKeyAsync(
            request.IdempotencyKey,
            cancellationToken)
        .ConfigureAwait(false);

        if (existingOrder is not null)
        {
            return existingOrder.Id;
        }

        if (request.Items.Count <= 0)
        {
            throw new ProjetoTopdown.Application.Exceptions.ValidationException(
                "O pedido deve conter ao menos um item.");
        }

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken)
            .ConfigureAwait(false);

        if (customer is null)
        {
            throw new NotFoundException($"Cliente com ID {request.CustomerId} não encontrado.");
        }

        var productIds = request.Items.Select(i => i.ProductId).ToList();

        var productsFromDb =
            await _productRepository.GetByIdsAsync(productIds, cancellationToken)
        .ConfigureAwait(false);

        var newOrder = new Order(request.CustomerId, request.IdempotencyKey);

        foreach (var itemDto in request.Items)
        {
            var product = productsFromDb.FirstOrDefault(p => p.Id == itemDto.ProductId);
            if (product is null)
            {
                throw new NotFoundException($"Produto com ID {itemDto.ProductId} não encontrado.");
            }

            if (itemDto.Quantity > product.StockQty)
            {
                throw new ProjetoTopdown.Application.Exceptions.ValidationException(
                    $"Estoque insuficiente para o produto '{product.Name}'." +
                    $" Disponível: {product.StockQty}, Solicitado: {itemDto.Quantity}.");
            }

            var orderItem = new OrderItem(newOrder.Id, product.Id, product.Price, itemDto.Quantity);
            newOrder.OrderItems.Add(orderItem);

            product.DecreaseStock(itemDto.Quantity);
        }

        newOrder.CalculateTotalAmount();

        await _orderRepository.AddAsync(newOrder, cancellationToken).ConfigureAwait(false);

        //Injeção do unitOfWork criado aqui para controlar todas as transações.
        //Deve ser implementado em todos os handler, mas não será devido o tempo.
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return newOrder.Id;
    }
}