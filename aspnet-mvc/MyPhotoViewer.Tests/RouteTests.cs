using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace MyPhotoViewer.Tests
{
    [TestFixture]
    public class RouteTests
    {
        [Test]
        public void TestRoutes()
        {
            TestRouteMatch("~/Album/Create", "Album", "Create", null, "GET");
            TestRouteMatch("~/Album/Create", "Album", "Create", null, "POST");

            //TestRouteMatch("~/Photo/1/Edit", "Photo", "Edit", new { photoId = "1" });
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Act
            RouteData resultRouteData = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //Assert
            Assert.IsNotNull(resultRouteData);
            Assert.IsTrue(CheckIncomingResultRouteData(resultRouteData, controller, action, routeProperties));
        }

        private bool CheckIncomingResultRouteData(RouteData resultRouteData, string controller, string action, object routeProperties = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) => {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(resultRouteData.Values["controller"], controller) && valCompare(resultRouteData.Values["action"], action);

            if (routeProperties != null)
            {
                PropertyInfo[] props = routeProperties.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (!(resultRouteData.Values.ContainsKey(prop.Name) && valCompare(resultRouteData.Values[prop.Name], prop.GetValue(routeProperties, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private HttpContextBase CreateHttpContext(string targetUrl = null, 
                                                 string httpMethod = "GET")
        {
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }
    }
}
