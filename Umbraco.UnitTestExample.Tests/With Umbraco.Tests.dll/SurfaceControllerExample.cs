using Moq;
using NUnit.Framework;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Profiling;
using Umbraco.Tests.TestHelpers;
using Umbraco.UnitTestExample.Web.Controllers;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace Umbraco.UnitTestExample.Tests.With_Umbraco.Tests.dll
{
    [TestFixture]
    public class SurfaceControllerExample// : BaseWebTest
    {
        [SetUp]
        public void Setup()
        {
            #region Step 2
            //GetUmbracoContext("http://localhost", -1, null, setSingleton: true);
            #endregion

            var appContext = new ApplicationContext(
                CacheHelper.CreateDisabledCacheHelper(),
                new ProfilingLogger(Mock.Of<ILogger>(), Mock.Of<IProfiler>()));

            var umbracoContext = UmbracoContext.EnsureContext(
                new Mock<HttpContextBase>().Object,
                appContext,
                new Mock<WebSecurity>(null, null).Object,
                Mock.Of<IUmbracoSettingsSection>(),
                Enumerable.Empty<IUrlProvider>(),
                replaceContext: true);
        }

        [Test]
        public void Umbraco_SimpleSurfaceController_Add()
        {
            const int expectedSum = 3;
            var model = new AdditionModel
            {
                X = 1,
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
