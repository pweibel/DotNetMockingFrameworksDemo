using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mocking
{
	public interface IUserGateway
	{
		bool Persist(User user);
	}
}
