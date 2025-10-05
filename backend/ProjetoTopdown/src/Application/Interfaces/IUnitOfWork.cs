using System.Threading;
using System.Threading.Tasks;

namespace ProjetoTopdown.Application.Interfaces;

public interface IUnitOfWork
{
    /// <summary>
    /// Salva todas as alterações feitas neste contexto no banco de dados.
    /// </summary>
    /// <returns>O número de registros afetados no banco de dados.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}