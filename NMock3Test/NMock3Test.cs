using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mocking;

using NMock2;

namespace NMock3Test
{
    [TestClass]
    public class NMock3Test
    {
        [TestMethod]
        public void TestPersist()
        {
            MockFactory factory = new MockFactory();

            //Create mocks
            Mock<IUserGateway> mockGateway = factory.CreateMock<IUserGateway>();
            Mock<IUserValidator> mockValidator = factory.CreateMock<IUserValidator>();

            //Create user
            User user = new User();

            //Expectations
            using(factory.Ordered)
            {
                mockValidator.Expects.One.MethodWith(m => m.Validate(user)).WillReturn(true);
                mockGateway.Expects.One.MethodWith(m => m.Persist(user)).WillReturn(true);
            }

            //Assign gateway
            user.Gateway = mockGateway.MockObject;

            //Test method
            Assert.AreEqual(true, user.Persist(mockValidator.MockObject));

            factory.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
