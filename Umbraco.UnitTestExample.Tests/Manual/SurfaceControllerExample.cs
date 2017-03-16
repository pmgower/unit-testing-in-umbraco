using Moq;
using NUnit.Framework;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Profiling;
using Umbraco.UnitTestExample.Web.Controllers;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace Umbraco.UnitTestExample.Tests.Manual_Umbraco_Tests
{
    /// <summary>
    /// This code was taken from Lars-Erik's github repo where he had some sample unit tests for Umbraco
    /// https://github.com/lars-erik/umbraco-unit-testing-samples
    /// </summary>
    [TestFixture]
    public class SurfaceControllerExample
    {
        #region Step 2
        [SetUp]
        public void Setup()
        {
            //var appContext = new ApplicationContext(
            //    CacheHelper.CreateDisabledCacheHelper(),
            //    new ProfilingLogger(Mock.Of<ILogger>(), Mock.Of<IProfiler>()));

            //var umbracoContext = UmbracoContext.EnsureContext(
            //    new Mock<HttpContextBase>().Object,
            //    appContext,
            //    new Mock<WebSecurity>(null, null).Object,
            //    Mock.Of<IUmbracoSettingsSection>(),
            //    Enumerable.Empty<IUrlProvider>(),
            //    replaceContext: true);
        }
        #endregion

        [Test]
        public void SimpleSurfaceController_Add()
        {
            const int expectedSum = 6;
            var model = new AdditionModel
            {
                X = 4,
                Y = 2,
                IsPosted = true
            };

            var controller = new SimpleSurfaceController();
            var result = controller.AddForm(model);
            var resultModel = (AdditionModel)result.Model;

            Assert.AreEqual(expectedSum, resultModel.Sum);
        }
    }
}
