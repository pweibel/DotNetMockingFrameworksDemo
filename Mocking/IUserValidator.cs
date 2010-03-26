using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mocking
{
	public interface IUserValidator
	{
		bool Validate(User user);
	}
}
