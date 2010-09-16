using FakeItEasy;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mocking;

namespace MockingTest
{
	[TestClass]
	public class FakeItEasyTest
	{
		[TestMethod]
		public void TestPersist()
		{
			// Arrange
			var gateway = A.Fake<IUserGateway>();
			var validator = A.Fake<IUserValidator>();
			User user = new User();
			user.Gateway = gateway;
			A.CallTo(() => validator.Validate(user)).Returns(true);
			A.CallTo(() => gateway.Persist(user)).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(true, bRet);
			A.CallTo(() => validator.Validate(user)).MustHaveHappened(Repeated.Once);
			A.CallTo(() => gateway.Persist(user)).MustHaveHappened(Repeated.Once);
		}

		[TestMethod]
		public void TestPersist_Not_Validated()
		{
			// Arrange
			var gateway = A.Fake<IUserGateway>();
			var validator = A.Fake<IUserValidator>();
			User user = new User();
			user.Gateway = gateway;
			A.CallTo(() => validator.Validate(user)).Returns(false);
			A.CallTo(() => gateway.Persist(user)).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(false, bRet);
			A.CallTo(() => validator.Validate(user)).MustHaveHappened(Repeated.Once);
			A.CallTo(() => gateway.Persist(user)).MustNotHaveHappened();
		}

		[TestMethod]
		public void TestPersist_GatewayClass()
		{
			// Arrange
			var gateway = A.Fake<UserGateway>();
			var validator = A.Fake<IUserValidator>();
			User user = new User();
			user.Gateway = gateway;
			A.CallTo(() => validator.Validate(user)).Returns(true);
			A.CallTo(() => gateway.Persist(user)).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(true, bRet);
			A.CallTo(() => validator.Validate(user)).MustHaveHappened(Repeated.Once);
			A.CallTo(() => gateway.Persist(user)).MustHaveHappened(Repeated.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(ExpectationException))]
		public void TestPersist_ValidatorClass()
		{
			// Arrange
			var gateway = A.Fake<UserGateway>();
			var validator = A.Fake<UserValidator>();
			User user = new User();
			user.Gateway = gateway;
			A.CallTo(() => validator.Validate(user)).Returns(true);
			A.CallTo(() => gateway.Persist(user)).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(true, bRet);
			A.CallTo(() => validator.Validate(user)).MustHaveHappened(Repeated.Once);
			A.CallTo(() => gateway.Persist(user)).MustHaveHappened(Repeated.Once);
		}
	}
}
