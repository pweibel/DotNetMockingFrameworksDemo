using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mocking;

using Simple.Mocking;

namespace MockingTest
{
    [TestClass]
    public class SimpleNetTest
    {
        [TestMethod]
        public void TestPersist()
        {
            //Create mocks
            var expectationScope = new ExpectationScope();
            IUserGateway mockGateway = Mock.Interface<IUserGateway>(expectationScope);
            IUserValidator mockValidator = Mock.Interface<IUserValidator>(expectationScope);

            //Create user
            User user = new User();

            //Expectations
            Expect.Once.MethodCall(() => mockValidator.Validate(user)).Returns(true);
            Expect.Once.MethodCall(() => mockGateway.Persist(user)).Returns(true);

            //Assign gateway
            user.Gateway = mockGateway;

            //Test method
            Assert.AreEqual(true, user.Persist(mockValidator));

            AssertExpectations.IsMetFor(expectationScope);
        }
    }
}
