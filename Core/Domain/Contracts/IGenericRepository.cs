using Domain.Models;

namespace Domain.Contracts
{
	public interface IGenericRepository<TEntity,TKey> where TEntity: BaseEntity<TKey>
	{
		//Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
		Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> spec,bool trackChanges = false);
		//Task<TEntity?> GetByIdAsync(TKey id);
		Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> spec);
		Task<int> CountAsync(ISpecification<TEntity,TKey> spec);
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

	}
}
