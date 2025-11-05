using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class DuplicatedEmailBadRequest(string email) : BadRequestException($"There is another User using this email {email} ")
	{
	}
}
