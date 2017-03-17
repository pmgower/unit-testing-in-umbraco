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

namespace Umbraco.UnitTestExample.Tests.With_Umbraco.Tests.dll
{
    /// <summary>
    /// This code was taken from Lars-Erik's github repo where he had some sample unit tests for Umbraco
    /// https://github.com/lars-erik/umbraco-unit-testing-samples
    /// </summary>
    [TestFixture]
    public class ApiControllerExample //: Umbraco.Tests.TestHelpers.BaseWebTest
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
        public void Umbraco_SimpleApiController_Subtract()
        {
            string expectedJson = "{\"statusCode\":200,\"message\":\"OK\",\"result\":2}";
            var model = new SubtrationModel
            {
                X = 4,
                Y = 2
            };

            var controller = new SimpleApiController();
            var result = controller.Subtract(model) as JsonStringResultExtension.CustomJsonStringResult;
            var actualJson = result.Json;
            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
