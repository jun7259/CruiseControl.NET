using NMock;
using NUnit.Framework;
using ThoughtWorks.CruiseControl.WebDashboard.Dashboard;
using ThoughtWorks.CruiseControl.WebDashboard.Dashboard.Actions;
using ThoughtWorks.CruiseControl.WebDashboard.MVC;
using ThoughtWorks.CruiseControl.WebDashboard.Plugins.FarmReport;

namespace ThoughtWorks.CruiseControl.UnitTests.WebDashboard.Dashboard.Actions
{
	[TestFixture]
	public class DefaultActionTest
	{
		private DefaultAction action;
		private DynamicMock linkFactoryMock;

		[SetUp]
		public void Setup()
		{
			linkFactoryMock = new DynamicMock(typeof(ILinkFactory));
			action = new DefaultAction((ILinkFactory) linkFactoryMock.MockInstance);
		}

		private void VerifyAll()
		{
			linkFactoryMock.Verify();
		}

		[Test]
		public void ShouldReturnRedirectToFarmReport()
		{
			IAbsoluteLink link = new GeneralAbsoluteLink("", "http://here");
			linkFactoryMock.ExpectAndReturn("CreateFarmLink", link, "", new ActionSpecifierWithName(FarmReportFarmPlugin.ACTION_NAME));

			IView view = action.Execute(null);

			Assert.AreEqual("http://here", ((RedirectView) view).ResponseFragment);
			VerifyAll();
		}
	}
}
