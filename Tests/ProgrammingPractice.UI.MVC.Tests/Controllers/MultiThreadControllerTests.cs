﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingPractice.UI.MVC.Controllers;

namespace ProgrammingPractice.Tests.Controllers
{
    [TestClass]
    public class MultiThreadControllerTests : Controller
    {
        [TestMethod]
        public void Index_ViewExists_ViewReturned()
        {
            // Arrange
            MultiThreadController controller = new MultiThreadController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
