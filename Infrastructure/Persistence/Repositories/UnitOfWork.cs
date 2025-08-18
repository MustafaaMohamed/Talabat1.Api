using Domain.Contracts;
using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TalabatDbContext _context;
		private readonly Dictionary<string, object> _repositories;

		public UnitOfWork(TalabatDbContext context)
		{
			_context = context;
			_repositories = new();
		}
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		{
			var type = typeof(TEntity).Name;
			if (!_repositories.ContainsKey(type))
			{
				var repository = new GenericRepository<TEntity, TKey>(_context);
				_repositories.Add(type, repository);
			}
			return (GenericRepository<TEntity, TKey>)_repositories[type] ;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
