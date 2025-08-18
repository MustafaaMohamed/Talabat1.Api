using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		private readonly TalabatDbContext _context;

		public GenericRepository(TalabatDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
		{
			if(trackChanges)
				return await _context.Set<TEntity>().ToListAsync();
			return await _context.Set<TEntity>().AsNoTracking().ToListAsync();

		}
		public async Task<TEntity?> GetByIdAsync(TKey id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}
		public async Task AddAsync(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
		}
		public void Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
		}
		public void Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
		}
	}
}
