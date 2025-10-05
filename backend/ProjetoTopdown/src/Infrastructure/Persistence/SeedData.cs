using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Infrastructure.Persistence;

#pragma warning disable CA1002 // Não expor listas genéricas
public static class SeedData
{
    public static List<Product> GenerateProducts(int count)
    {
        var products = new List<Product>();
        var creationDate = new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc);

        for (int i = 1; i <= count; i++)
        {
            var product = new Product(
                $"Produto de Teste {i}",
                $"SKU-{1000 + i}",
                (decimal)(i * 10.5 + 20),
                i * 5
            );

            typeof(Product).GetProperty("Id")?.SetValue(product, i);
            typeof(Product).GetProperty("CreatedAt")?.SetValue(product, creationDate);

            products.Add(product);
        }
        return products;
    }

    public static List<Customer> GenerateCustomers(int count)
    {
        var customers = new List<Customer>();
        var creationDate = new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc);

        for (int i = 1; i <= count; i++)
        {
            var customer =
                new Customer(
                    $"Cliente de Teste {i}", $"cliente{i}@email.com",
                    $"{i}{i}{i}.{i}{i}{i}.{i}{i}{i}-{i}{i}");
            typeof(Customer).GetProperty("Id")?.SetValue(customer, i);
            typeof(Customer).GetProperty("CreatedAt")?.SetValue(customer, creationDate);
            customers.Add(customer);
        }
        return customers;
    }
}