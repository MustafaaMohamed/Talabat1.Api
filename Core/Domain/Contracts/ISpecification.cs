using Domain.Models;
using System.Linq.Expressions;
namespace Domain.Contracts
{ 
public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
	Expression<Func<TEntity, bool>>? Criteria { get; set; }
	List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }
	Expression<Func<TEntity, object>>? OrderBy { get; set; }
	Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
	int Skip { get; set; }
	int Take { get; set; }
	bool IsPagination { get; set; }
}
}

