namespace Mocking
{
	public class UserGateway : IUserGateway
	{
		#region IUserGateway Members
		public virtual bool Persist(User user)
		{
			return true;
		}
		#endregion
	}
}
