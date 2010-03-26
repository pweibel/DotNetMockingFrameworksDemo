using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using TypeMock;

namespace MockingTest
{
	 ///<summary>
	 /// Vorteile: Keine Interfaces notwendig, mächtiges Framework, beide Patterns unterstützt (reflective mocking, natural mocking)
	 /// Nachteile: Interfaces nicht unterstützt, nicht frei
	 /// </summary>
	[TestClass]
	public class TypeMockTest
	{
	    [TestMethod]
	    public void TestPersistWithNaturalMocking()
	    {
	        MockManager.Init();

	        //Unschön, muss hier aber so gemacht werden
	        User user = new User();

	        // Natural mocking (Vorteil: Methoden müssen nicht als Strings angegeben werden -> Refactoring)
	        using(RecordExpectations recorder = new RecordExpectations())
	        {
	            IUserGateway mockGateway = new UserGateway();
	            IUserValidator mockValidator = new UserValidator();
				
	            //Expectations
	            recorder.ExpectAndReturn(mockValidator.Validate(user), true);
	            recorder.ExpectAndReturn(mockGateway.Persist(user), true);
	        }

	        //Instanzen erstellen
	        IUserGateway gateway = new UserGateway();
	        IUserValidator validator = new UserValidator();
			
	        //Gateway über Setter-Injection zuweisen
	        user.Gateway = gateway;

	        //Methode
	        Assert.AreEqual(true, user.Persist(validator));

	        MockManager.Verify();
	    }
		
	    [TestMethod]
	    public void TestPersistWithReflectiveMocking()
	    {
	        MockManager.Init();

	        //Mocks erstellen
	        Mock mockGateway = MockManager.Mock<UserGateway>();
	        Mock mockValidator = MockManager.Mock<UserValidator>();

	        //User erstellen
	        UserGateway gateway = new UserGateway();
	        IUserValidator validator = new UserValidator();
	        User user = new User(gateway);

	        //Expectations
	        mockValidator.ExpectAndReturn("Validate", true).Args(user);
	        mockGateway.ExpectAndReturn("Persist", true).Args(user);

	        //Methode
	        Assert.AreEqual(true, user.Persist(validator));

	        MockManager.Verify();
	    }
	}
}
