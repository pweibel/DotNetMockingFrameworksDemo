using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mocking;

using NSubstitute;

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
	}
}
