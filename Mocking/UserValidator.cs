namespace Mocking
{
	public class UserValidator : IUserValidator
	{
		#region IUserValidator Members
		public bool Validate(User user)
		{
			if(user == null) return false;

			return true;
		}
		#endregion
	}
}
