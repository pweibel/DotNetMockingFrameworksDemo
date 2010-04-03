using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using Moq;

namespace MockingTest
{
	[TestClass]
	public class MoqTest
	{
		[TestMethod]
		public void TestPersist()
		{
			var gateway = new Mock<IUserGateway>();
			var validator = new Mock<IUserValidator>();

			//Create user
			User user = new User();

			//Excpectations
			validator.Setup(x => x.Validate(user)).Returns(true).AtMostOnce();
			gateway.Setup(x => x.Persist(user)).Returns(true).AtMostOnce();

			//Assign gateway
			user.Gateway = gateway.Object;

			//Test method
			Assert.AreEqual(true, user.Persist(validator.Object));

			validator.VerifyAll();
			gateway.VerifyAll();
		}

		[TestMethod]
		public void TestPersistWithFactory()
		{
			MockFactory factory = new MockFactory(MockBehavior.Strict);

			//Classes work, methods have to be virtual -> not nice
			var mockGateway = factory.Create<UserGateway>();
			var mockValidator = factory.Create<IUserValidator>();

			User user = new User();

			//Excpectations
			mockValidator.Setup(x => x.Validate(user)).Returns(true).AtMostOnce();
			mockGateway.Setup(x => x.Persist(user)).Returns(true).AtMostOnce();

			//Assign gateway
			user.Gateway = mockGateway.Object;

			//Test method
			Assert.AreEqual(true, user.Persist(mockValidator.Object));

			factory.VerifyAll();
		}
	}
}
