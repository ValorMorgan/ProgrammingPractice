using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingPractice.UI.MVC.Controllers;

namespace ProgrammingPractice.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_ViewExists_ViewReturned()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ViewExists_ViewReturned()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Author_ViewExists_ViewReturned()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Author() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
