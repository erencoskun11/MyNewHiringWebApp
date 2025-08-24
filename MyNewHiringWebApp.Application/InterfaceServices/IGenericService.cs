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
        Task<(IEnumerable<TDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);

        Task<bool> CreateAsync(TCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, TUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
