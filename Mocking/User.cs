namespace Mocking
{
	public class User
	{
		public IUserGateway Gateway
		{
			get; set;
		}

		public User()
		{
			this.Gateway = new UserGateway();
		}

		public User(IUserGateway gateway)
		{
			this.Gateway = gateway;
		}

		public bool Persist(IUserValidator validator)
		{
			bool bValid = validator.Validate(this);

			if(bValid) bValid = this.Gateway.Persist(this);

			return bValid;
		}
	}
}
