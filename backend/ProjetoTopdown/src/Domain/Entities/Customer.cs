namespace ProjetoTopdown.Domain.Entities;

#pragma warning disable CS8618 //Considere adicionar o modificador "obrigatório".
public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Document { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Customer() { }

    public Customer(string name, string email, string document)
    {
        Name = name;
        Email = email;
        Document = document;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string email, string document)
    {
        Name = name;
        Email = email;
        Document = document;
    }
}