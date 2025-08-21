﻿using Domain.Contracts;
using Domain.Models;
using System.Linq.Expressions;

namespace Services.Specifications
{
	public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; }
		public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new();
		public Expression<Func<TEntity, object>>? OrderBy { get; set; }
		public Expression<Func<TEntity, object>>? OrderByDescending { get; set ; }
		public int Skip { get; set; }
		public int Take { get ; set; }
		public bool IsPagination { get; set; }

		public BaseSpecification(Expression<Func<TEntity, bool>> expression)
		{
			Criteria = expression;
		}
		protected void AddInclude(Expression<Func<TEntity, object>> expression)
		{
			IncludeExpressions.Add(expression);
		}
		protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
		{
			OrderBy = expression;
		}
		protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
		{
			OrderByDescending = expression;
		}
		protected void ApplyPagination(int pageIndex,int pageSize)
		{
			IsPagination = true;
			Take = pageSize;
			Skip = (pageIndex-1)*pageSize;

		}
	}
}
