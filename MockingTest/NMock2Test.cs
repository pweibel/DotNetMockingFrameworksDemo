using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using NMock2;

namespace MockingTest
{
	[TestClass]
	public class NMock2Test
	{
		[TestMethod]
		public void TestPersist()
		{
			Mockery mocks = new Mockery();

			//Crate mocks
			IUserGateway mockGateway = mocks.NewMock<IUserGateway>();
			IUserValidator mockValidator = mocks.NewMock<IUserValidator>();

			//Create user
			User user = new User();

			//Expectations
			using(mocks.Ordered)
			{
				Expect.Once.On(mockValidator).Method("Validate").With(user).Will(Return.Value(true));
				Expect.Once.On(mockGateway).Method("Persist").With(user).Will(Return.Value(true));
			}

			//Assign gateway
			user.Gateway = mockGateway;

			//Test method
			Assert.AreEqual(true, user.Persist(mockValidator));

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
	}
}
