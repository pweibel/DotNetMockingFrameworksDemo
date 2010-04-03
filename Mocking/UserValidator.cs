namespace Mocking
{
	public class UserValidator : IUserValidator
	{
		public bool Validate(User user)
		{
			if(user == null) return false;

			return true;
		}
	}
}
