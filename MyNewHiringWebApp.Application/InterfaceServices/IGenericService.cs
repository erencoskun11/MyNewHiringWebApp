using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class
    {
        Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default);
        Task<TDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(TCreateDto dto, CancellationToken ct = default); // önemli: Task<int>
        Task UpdateAsync(int id, TUpdateDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
