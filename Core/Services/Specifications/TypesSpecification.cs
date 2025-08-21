using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
	public class TypesSpecification : BaseSpecification<ProductType, int>
	{
		public TypesSpecification() : base(null)
		{
		}
	}
}
