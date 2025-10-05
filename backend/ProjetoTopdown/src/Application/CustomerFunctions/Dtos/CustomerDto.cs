namespace ProjetoTopdown.Application.CustomerFunctions.Dtos;

/// <summary>
/// DTO para representar os dados de um cliente que serão enviados pela API.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// O ID do cliente.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// O nome do cliente.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// O email do cliente.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// O documento (CPF/CNPJ) do cliente.
    /// </summary>
    public string Document { get; set; } = string.Empty;
}