using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace MockingTest
{
	[TestClass]
	public class TypeMockTest
	{
	    [TestMethod]
	    public void TestPersistWithNaturalMocking()
	    {
	        MockManager.Init();

	        //Create User
	        User user = new User();

	        // Natural mocking
	        using(RecordExpectations recorder = new RecordExpectations())
	        {
	            IUserGateway mockGateway = new UserGateway();
	            IUserValidator mockValidator = new UserValidator();
				
	            //Expectations
	            recorder.ExpectAndReturn(mockValidator.Validate(user), true);
	            recorder.ExpectAndReturn(mockGateway.Persist(user), true);
	        }

	        //Create instances
	        IUserGateway gateway = new UserGateway();
	        IUserValidator validator = new UserValidator();
			
	        //Assign gateway
	        user.Gateway = gateway;

	        //Test method
	        Assert.AreEqual(true, user.Persist(validator));

	        MockManager.Verify();
	    }
		
	    [TestMethod]
	    public void TestPersistWithReflectiveMocking()
	    {
	        MockManager.Init();

	        //Create mocks
	        Mock mockGateway = MockManager.Mock<UserGateway>();
	        Mock mockValidator = MockManager.Mock<UserValidator>();

	        //Create user
	        UserGateway gateway = new UserGateway();
	        IUserValidator validator = new UserValidator();
	        User user = new User(gateway);

	        //Expectations
	        mockValidator.ExpectAndReturn("Validate", true).Args(user);
	        mockGateway.ExpectAndReturn("Persist", true).Args(user);

	        //Test method
	        Assert.AreEqual(true, user.Persist(validator));

	        MockManager.Verify();
	    }

		[TestMethod]
		[Isolated]
		public void TestPersistWithAAA()
		{
			//Arrange
			//Create mocks
			UserGateway gateway = Isolate.Fake.Instance<UserGateway>();
			IUserValidator validator = Isolate.Fake.Instance<IUserValidator>();
            //Create user
			User user = new User(gateway);
            //Expectations
			Isolate.WhenCalled(() => validator.Validate(user)).WillReturn(true);
			Isolate.WhenCalled(() => gateway.Persist(user)).WillReturn(true);

			//Act
			bool bPersist = user.Persist(validator);

			//Assert
			Assert.AreEqual(true, bPersist);
		}
	}
}
