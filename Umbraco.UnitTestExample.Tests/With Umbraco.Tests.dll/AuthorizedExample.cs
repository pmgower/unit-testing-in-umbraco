using Moq;
using NUnit.Framework;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Profiling;
using Umbraco.Core.Security;
using Umbraco.Core.Services;
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
    public class AuthorizedExample : Umbraco.Tests.TestHelpers.BaseDatabaseFactoryTest
    {
        private UmbracoContext _umbracoContext;
        private ServiceContext _serviceContext;

        [SetUp]
        public void Setup()
        {
            #region Step 2
            base.Initialize();
            this._umbracoContext = GetUmbracoContext("http://localhost", -1, null, true);
            this._serviceContext = Umbraco.Tests.TestHelpers.MockHelper.GetMockedServiceContext();
            #endregion

            //var appContext = new ApplicationContext(
            //    CacheHelper.CreateDisabledCacheHelper(),
            //    new ProfilingLogger(Mock.Of<ILogger>(), Mock.Of<IProfiler>()));

            //this._umbracoContext = UmbracoContext.EnsureContext(
            //    new Mock<HttpContextBase>().Object,
            //    appContext,
            //    new Mock<WebSecurity>(null, null).Object,
            //    Mock.Of<IUmbracoSettingsSection>(),
            //    Enumerable.Empty<IUrlProvider>(),
            //    replaceContext: true);

            //this._serviceContext = new ServiceContext(
            //    new Mock<IContentService>().Object,
            //    new Mock<IMediaService>().Object,
            //    new Mock<IContentTypeService>().Object,
            //    new Mock<IDataTypeService>().Object,
            //    new Mock<IFileService>().Object,
            //    new Mock<ILocalizationService>().Object,
            //    new Mock<IPackagingService>().Object,
            //    new Mock<IEntityService>().Object,
            //    new Mock<IRelationService>().Object,
            //    new Mock<IMemberGroupService>().Object,
            //    new Mock<IMemberTypeService>().Object,
            //    new Mock<IMemberService>().Object,
            //    new Mock<IUserService>().Object,
            //    new Mock<ISectionService>().Object,
            //    new Mock<IApplicationTreeService>().Object,
            //    new Mock<ITagService>().Object,
            //    new Mock<INotificationService>().Object,
            //    new Mock<ILocalizedTextService>().Object,
            //    new Mock<IAuditService>().Object,
            //    new Mock<IDomainService>().Object,
            //    new Mock<ITaskService>().Object,
            //    new Mock<IMacroService>().Object);
        }

        [Test]
        public void SimpleAuthorizedController_CheckListValues()
        {
            //Arrange
            var simpleAuthController = new SimpleAuthorizedController();
            var expectedList = new[] { "X", "Y", "Z", "A", "B", "C" };

            //Act
            var actualList = simpleAuthController.List();

            //Assert
            Assert.That(actualList, Is.EquivalentTo(expectedList));
        }

        [Test]
        public void SimpleAuthorizedController_CheckUserName()
        {
            //Arrange
            string expectedResult = "Authorized Username";

            int testingUserId = 1;
            var stubbedUmbracoIdentity = GetStubbedUmbracoIdentity(testingUserId);
            var stubbedPrincipal = new ClaimsPrincipal(stubbedUmbracoIdentity);
            Mock.Get(this._umbracoContext.HttpContext).Setup(context => context.User).Returns(stubbedPrincipal);

            var user = Mock.Of<IUser>();
            Mock.Get(user).Setup(u => u.Name).Returns(expectedResult);
            Mock.Get(this._serviceContext.UserService).Setup(context => context.GetUserById(testingUserId)).Returns(user);

            //Act
            var simpleAuthController = new SimpleAuthorizedController();
            var actualResult = simpleAuthController.GetUserInfo();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        private UmbracoBackOfficeIdentity GetStubbedUmbracoIdentity(int userId)
        {
            var sessionId = System.Guid.NewGuid().ToString();
            var userData = new UserData(sessionId)
            {
                AllowedApplications = new[] { "content", "media" },
                Culture = "en-us",
                Id = userId,
                RealName = "Test Identity",
                Roles = new[] { "admin" },
                StartContentNode = -1,
                StartMediaNode = 320,
                Username = "testIdentity"
            };

            var identity = new UmbracoBackOfficeIdentity(userData);
            return identity;
        }
    }
}
