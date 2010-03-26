using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using NUnit.Mocks;

namespace MockingTest
{
	/// <summary>
	/// In NUnit gibt es ebenfalls ein Mocking-Framework, aber offenbar nur für den internen Gebrauch. Es werden Interfaces gemockt. 
	/// Klassen können nur gemockt werden wenn sie von MarshalByRefObject ableiten.
	/// Vorteile: sehr einfach, falls NUnit eingesetzt wird muss kein weiteres Mocking-Framwork eingesetzt werden
	/// Nachteile: Nicht offiziell, keine Dokumentation, Methoden müssen als Strings angegeben werden, nur Interfaces werden sauber unterstützt
	/// </summary>
	[TestClass]
	public class NUnitMockTest
	{
		[TestMethod]
		public void TestPersist()
		{
			//Gateway
			DynamicMock mockGateway = new DynamicMock(typeof(IUserGateway));
			IUserGateway gateway = (IUserGateway) mockGateway.MockInstance;
			
			//Validator
			DynamicMock mockValidator = new DynamicMock(typeof(IUserValidator));
			IUserValidator validator = (IUserValidator)mockValidator.MockInstance;

			//User
			User user = new User(gateway);

			//Expectations
			//mockValidator.Expect("Validate");
			mockValidator.ExpectAndReturn("Validate", true, user);
			mockGateway.ExpectAndReturn("Persist", true, user);

			Assert.AreEqual(true, user.Persist(validator));
			mockValidator.Verify();
			mockGateway.Verify();
		}
	}
}
