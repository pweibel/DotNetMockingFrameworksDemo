using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using Moq;

namespace MockingTest
{
	[TestClass]
	public class MoqTest
	{
		[TestMethod]
		public void TestPersist1()
		{
			var gateway = new Mock<IUserGateway>();
			var validator = new Mock<IUserValidator>();

			User user = new User();

			//Excpectations
			validator.Expect(x => x.Validate(user)).Returns(true);
			gateway.Expect(x => x.Persist(user)).Returns(true);

			//Gateway setzen
			user.Gateway = gateway.Object;

			//Methode testen
			Assert.AreEqual(true, user.Persist(validator.Object));

			validator.VerifyAll();
			gateway.VerifyAll();
		}

		[TestMethod]
		public void TestPersist2()
		{
			MockFactory factory = new MockFactory(MockBehavior.Strict);

			//Klassen gehen, Methoden müssen jedoch virtual sein -> unschön
			var mockGateway = factory.Create<UserGateway>();
			var mockValidator = factory.Create<IUserValidator>();

			User user = new User();

			//Excpectations
			mockValidator.Expect(x => x.Validate(user)).Returns(true);
			mockGateway.Expect(x => x.Persist(user)).Returns(true);

			//Gateway setzen
			user.Gateway = mockGateway.Object;

			//Methode testen
			Assert.AreEqual(true, user.Persist(mockValidator.Object));

			factory.VerifyAll();
		}
	}
}
