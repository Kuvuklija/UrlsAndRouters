using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Reflection;

namespace UrlsAndRouters.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl=null, string httpMethod = "GET") {

            //создать имитированный запрос
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            //создать имитированный ответ ---> ДЛЯ ТЕСТА ИСХОДЯЩИХ URL
            Mock<HttpResponseBase> mockResponce = new Mock<HttpResponseBase>();
            mockResponce.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            //создать имитированный контекст, используя запрос и ответ
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponce.Object);

            return mockContext.Object;
        }

        //тестируем правильный маршрут
        private void TestRouteMatch(string url, string controller, string action, object routeProperties=null, string httpMethod = "GET") {
            //arange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes); //class and method are defines in the folder "App_Start". The routes templates too.

            //action
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null) {
            Func<object, object, bool> valCompare = (v1, v2) =>{
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["action"], action) && valCompare(routeResult.Values["controller"], controller);

            if (propertySet != null) {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach(PropertyInfo pi in propInfo) {
                    if (!(routeResult.Values.ContainsKey(pi.Name) 
                        && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))){

                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        //тестируем неправильный маршрут
        private void TestRouteFail(string url) {
            //arrived
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //action
            RouteData result = routes.GetRouteData(CreateHttpContext(url)); //сопоставляет с шаблоном в файле RouteConfig ?

            //assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestIncomingRoutes() {
            //проверить Url, который надеемся получить
            //TestRouteMatch("~/", "Home", "Index");
            //TestRouteMatch("~/Customer", "Customer", "Index");
            //TestRouteMatch("~/Customer/List", "Customer", "List");
            //TestRouteMatch("~/Public", "Customer", "List");
            //TestRouteMatch("~/Shop/Index", "Home", "Index");
            TestRouteMatch("~/", "Home", "Index", new { id = "DefaultId" });
            TestRouteMatch("~/Customer", "Customer", "Index", new { id = "DefaultId"});
            TestRouteMatch("~/Customer/List", "Customer", "List", new { id = "DefaultId" });
            TestRouteMatch("~/Customer/List/Hello", "Customer", "List", new { id = "Hello" });
            TestRouteFail("~/Customer/List/Hello/Vasja");

            //проверить значения, получаемые из сегментов
            //TestRouteMatch("~/One/Two", "One", "Two");

            //нехватка или излишек сегментов приводит к ошибке
            //TestRouteFail("~/Customer/List/Segment");
            //TestRouteFail("~/Admin");

        }
    }
}
