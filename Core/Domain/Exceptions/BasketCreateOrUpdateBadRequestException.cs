using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class BasketCreateOrUpdateBadRequestException() : BadRequestException($"Invalid Opertaion When Trying to Create or Update Basket")
	{
	}
}
