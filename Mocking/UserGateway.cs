namespace Mocking
{
	public class UserGateway : IUserGateway
	{
		public virtual bool Persist(User user)
		{
			return true;
		}
	}
}
