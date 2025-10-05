namespace ProjetoTopdown.WebApi.Models;

#pragma warning disable CA1000 // Não declarar membros estáticos em tipos genéricos
public class ApiResponse<T>
{
    public int CodRetorno { get; set; }
    public string? Mensagem { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string? mensagem = null)
    {
        return new ApiResponse<T>
        {
            CodRetorno = 0,
            Mensagem = mensagem,
            Data = data
        };
    }
}


public static class ApiResponse
{
    public static ApiResponse<object> Error(string? mensagem)
    {
        return new ApiResponse<object>
        {
            CodRetorno = 1,
            Mensagem = mensagem,
            Data = null
        };
    }
}