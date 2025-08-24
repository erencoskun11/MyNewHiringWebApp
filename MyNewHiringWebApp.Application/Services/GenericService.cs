using AutoMapper;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services
{
    public class GenericService<TEntity, TDto, TCreateDto, TUpdateDto> :
        IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repo;
        protected readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<TDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity == null ? default : _mapper.Map<TDto>(entity);
        }

        public virtual async Task<(IEnumerable<TDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        {
            var (items, total) = await _repo.GetPagedAsync(page, pageSize, null, ct);
            var dtos = _mapper.Map<IEnumerable<TDto>>(items);
            return (dtos, total);
        }


        public virtual async Task<bool> CreateAsync(TCreateDto dto, CancellationToken ct = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map<TEntity>(dto);
            await _repo.AddAsync(entity, ct);

            await _repo.SaveChangesAsync(ct); 

            return true;
        }
        public virtual async Task<bool> UpdateAsync(int id, TUpdateDto dto, CancellationToken ct = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing == null) return false;
            _mapper.Map(dto, existing);

            _repo.Update(existing);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing == null) return false;

            _repo.Remove(existing);
            return true;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default)
        {
            var extings = await _repo.ListAsync(ct);
            return _mapper.Map<IEnumerable<TDto>>(extings);
        }
    }
}

