using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking;
using Rhino.Mocks;

namespace MockingTest
{
	[TestClass]
	public class RhinoMockTest
	{
		[TestMethod]
		public void TestPersistWithRecordingReplay()
		{
			MockRepository mocks = new MockRepository();

			IUserGateway gateway = mocks.StrictMock<IUserGateway>();
			IUserValidator validator = mocks.StrictMock<IUserValidator>();

			//User erstellen
			User user = new User();

			//Expectations
			using(mocks.Ordered())
			{
				Expect.Call(validator.Validate(user)).Return(true);
				Expect.Call(gateway.Persist(user)).Return(true);
			}

			//Recording stoppen, Replay starten
			mocks.ReplayAll();

			//Gateway zuweisen
			user.Gateway = gateway;

			//Methode testen
			Assert.AreEqual(true, user.Persist(validator));

			mocks.VerifyAll();
		}

		[TestMethod]
		public void TestPersistWithArrangeActAssert()
		{
			var gateway = MockRepository.GenerateMock<IUserGateway>();
			var validator = MockRepository.GenerateMock<IUserValidator>();

			//User erstellen
			User user = new User();

			//Expectations
			validator.Expect(x => x.Validate(user)).Return(true);
			gateway.Expect(x => x.Persist(user)).Return(true);

			//Gateway zuweisen
			user.Gateway = gateway;

			//Methode testen
			Assert.AreEqual(true, user.Persist(validator));

			//Asserts
			validator.VerifyAllExpectations();
			gateway.VerifyAllExpectations();
		}
	}
}
