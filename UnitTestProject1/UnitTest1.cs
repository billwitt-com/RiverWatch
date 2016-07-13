using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IncomingICPWebApplication.Controllers;
using IncomingICPWebApplication.Models;




namespace webApiTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new InboundICPFinalsController(); // the controller class
            var result = controller.PostInboundICPFinal(null); 
        }
    }
}
