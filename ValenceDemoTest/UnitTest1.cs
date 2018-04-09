using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValenceDemo.Controllers;
using System.Net;
using ValenceDemo.Models;
using System.Web.Http.Results;
using System.Web.Http;

namespace ValenceDemoTest.te
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Gloabal variable and decalration of object.As will test multiple test cases.
        /// </summary>
        ContactServiceController servicecontroller = new ContactServiceController();

        /// <summary>
        /// Created simple test case for only one api method for just given demo that we do like wise
        /// </summary>
        [TestMethod]
        public async void GetById()
        {
            var res = servicecontroller.GetContactDetail("ar@gmail.com");
            var contentresult = await res as OkNegotiatedContentResult<ContactDetail>;
            Assert.IsNotNull(contentresult);
            Assert.IsNotNull(contentresult.Content);
            Assert.AreEqual("ar@gmail.com", contentresult.Content.Email);
        }

    }
}
