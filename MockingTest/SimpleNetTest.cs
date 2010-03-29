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
            //Mocks erzeugen
            var expectationScope = new ExpectationScope();
            IUserGateway mockGateway = Mock.Interface<IUserGateway>(expectationScope);
            IUserValidator mockValidator = Mock.Interface<IUserValidator>(expectationScope);

            //User erstellen
            User user = new User();

            //Expectations
            Expect.Once.MethodCall(() => mockValidator.Validate(user)).Returns(true);
            Expect.Once.MethodCall(() => mockGateway.Persist(user)).Returns(true);

            //Gateway zuweisen
            user.Gateway = mockGateway;

            //Methode testen
            Assert.AreEqual(true, user.Persist(mockValidator));

            AssertExpectations.IsMetFor(expectationScope);
        }
    }
}
