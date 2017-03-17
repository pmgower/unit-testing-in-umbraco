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
    public class ApiControllerExample
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
        public void SimpleApiController_Subtract()
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
