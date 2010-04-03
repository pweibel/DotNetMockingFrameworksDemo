using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using NUnit.Mocks;

namespace MockingTest
{
	[TestClass]
	public class NUnitMockTest
	{
		[TestMethod]
		public void TestPersist()
		{
			//Gateway
			DynamicMock mockGateway = new DynamicMock(typeof(IUserGateway));
			IUserGateway gateway = (IUserGateway) mockGateway.MockInstance;
			
			//Validator
			DynamicMock mockValidator = new DynamicMock(typeof(IUserValidator));
			IUserValidator validator = (IUserValidator)mockValidator.MockInstance;

			//User
			User user = new User(gateway);

			//Expectations
			mockValidator.ExpectAndReturn("Validate", true, user);
			mockGateway.ExpectAndReturn("Persist", true, user);

			Assert.AreEqual(true, user.Persist(validator));
			mockValidator.Verify();
			mockGateway.Verify();
		}
	}
}
