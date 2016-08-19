using System;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingPractice.UI.MVC.Controllers.Ajax_Controllers;

namespace ProgrammingPractice.Tests.Controllers.Ajax_Controllers
{
    [TestClass]
    public class MultiThreadAjaxControllerTests
    {
        [TestMethod]
        public void SingleThread_AcceptsHTTPGetOnly_HasAttribute()
        {
            MethodBase method = typeof(MultiThreadAjaxController).GetMethod("SingleThread");

            Assert.IsNull(method.GetCustomAttribute(typeof(HttpGetAttribute)), "SingleThread cannot have the [HttpGet] attribute.");
            Assert.IsNotNull(method.GetCustomAttribute(typeof(HttpPostAttribute)), "SingleThread needs to have the [HttpPost] attribute.");
        }

        [TestMethod]
        public void MultiThread_AcceptsHTTPGetOnly_HasAttribute()
        {
            MethodBase method = typeof(MultiThreadAjaxController).GetMethod("MultiThread");

            Assert.IsNull(method.GetCustomAttribute(typeof(HttpGetAttribute)), "MultiThread cannot have the [HttpGet] attribute.");
            Assert.IsNotNull(method.GetCustomAttribute(typeof(HttpPostAttribute)), "MultiThread needs to have the [HttpPost] attribute.");
        }
    }
}
