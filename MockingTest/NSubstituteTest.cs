using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mocking;

using NSubstitute;
using NSubstitute.Exceptions;

namespace MockingTest
{
	[TestClass]
	public class NSubstituteTest
	{
		[TestMethod]
		public void TestPersist()
		{
			// Arrange
			var gateway = Substitute.For<IUserGateway>();
			var validator = Substitute.For<IUserValidator>();
			User user = new User();
			user.Gateway = gateway;
			validator.Validate(user).Returns(true);
			gateway.Persist(user).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(true, bRet);
			validator.Received().Validate(user);
			gateway.Received().Persist(user);
		}

		[TestMethod]
		public void TestPersist_Not_Validated()
		{
			// Arrange
			var gateway = Substitute.For<IUserGateway>();
			var validator = Substitute.For<IUserValidator>();
			User user = new User();
			user.Gateway = gateway;
			validator.Validate(user).Returns(false);
			gateway.Persist(user).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(false, bRet);
			validator.Received().Validate(user);
			gateway.DidNotReceive().Persist(user);
		}

		[TestMethod]
		public void TestPersist_GatewayClass()
		{
			// Arrange
			var gateway = Substitute.For<UserGateway>();
			var validator = Substitute.For<IUserValidator>();
			User user = new User();
			user.Gateway = gateway;
			validator.Validate(user).Returns(true);
			gateway.Persist(user).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(true, bRet);
			validator.Received().Validate(user);
			gateway.Received().Persist(user);
		}

		[TestMethod]
		[ExpectedException(typeof(SubstituteException))]
		public void TestPersist_ValidatorClass()
		{
			// Arrange
			var gateway = Substitute.For<UserGateway>();
			var validator = Substitute.For<UserValidator>();
			User user = new User();
			user.Gateway = gateway;
			validator.Validate(user).Returns(true);
			gateway.Persist(user).Returns(true);

			// Act
			bool bRet = user.Persist(validator);

			// Assert
			Assert.AreEqual(true, bRet);
			validator.Received().Validate(user);
			gateway.Received().Persist(user);
		}
	}
}
