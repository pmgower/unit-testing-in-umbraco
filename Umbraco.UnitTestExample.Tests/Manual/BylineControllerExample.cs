using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Profiling;
using Umbraco.UnitTestExample.Web.Controllers;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace Umbraco.UnitTestExample.Tests.Manual_Umbraco_Tests
{
    [TestFixture]
    public class BylineControllerExample
    {
        private UmbracoContext _umbracoContext;
        private UmbracoHelper _umbracoHelper;

        [SetUp]
        public void Setup()
        {
            var appContext = new ApplicationContext(
                CacheHelper.CreateDisabledCacheHelper(),
                new ProfilingLogger(Mock.Of<ILogger>(), Mock.Of<IProfiler>()));

            this._umbracoContext = UmbracoContext.EnsureContext(
                new Mock<HttpContextBase>().Object,
                appContext,
                new Mock<WebSecurity>(null, null).Object,
                Mock.Of<IUmbracoSettingsSection>(),
                Enumerable.Empty<IUrlProvider>(),
                replaceContext: true);

            var content = Mock.Of<IPublishedContent>();
            this._umbracoHelper = new UmbracoHelper(this._umbracoContext, content);
        }

        [Test]
        public void Built_In_Properties_Are_Used_By_Default()
        {
            const string expectedAuthor = "An author";
            const string expectedDate = "2015-12-31";
            var content = Mock.Of<IPublishedContent>();
            var contentMock = Mock.Get(content);
            contentMock.Setup(c => c.WriterName).Returns(expectedAuthor);
            contentMock.Setup(c => c.CreateDate).Returns(DateTime.Parse(expectedDate));
            var bylineController = new BylineController(_umbracoContext, _umbracoHelper);
            var result = bylineController.Byline();
            var model = (BylineModel)result.Model;

            Assert.AreEqual(expectedAuthor, model.Author);
            Assert.AreEqual(expectedDate, model.Date.Value.ToString("yyyy-MM-dd"));
        }
    }
}
