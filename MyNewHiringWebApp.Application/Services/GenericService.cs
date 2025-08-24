using AutoMapper;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public abstract class GenericService<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repo;
        protected readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repo.ListAsync(ct);
            return _mapper.Map<IEnumerable<TDto>>(list);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity == null ? default : _mapper.Map<TDto>(entity);
        }

        // ÖNEMLİ: interface Task<int> bekliyor — burada int id döndürüyoruz.
        public virtual async Task<int> CreateAsync(TCreateDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            // varsayım: Entity'nin 'Id' property'i var ve int tipinde.
            var idProp = entity.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            if (idProp != null)
            {
                var value = idProp.GetValue(entity);
                if (value is int intId) return intId;
                try
                {
                    return Convert.ToInt32(value);
                }
                catch { /* dönüşüm başarısız olursa aşağıya geç */ }
            }

            // Eğer Id bulunmazsa -1 veya 0 döndür (gerektiğinde özelleştir)
            return 0;
        }

        public virtual async Task UpdateAsync(int id, TUpdateDto dto, CancellationToken ct = default)
        {
            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing == null) throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with id {id} not found.");

            // Map dto onto existing entity (AutoMapper.Map<TSource, TDestination>(source, destination))
            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveChangesAsync(ct);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing == null) throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with id {id} not found.");
            _repo.Remove(existing);
            await _repo.SaveChangesAsync(ct);
        }
    }
}

