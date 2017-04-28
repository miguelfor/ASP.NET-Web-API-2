using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

 
 
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingApi.Models;
using System.Web.Script.Serialization;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
             
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(http://pruebas.apimedic.personalsoft.net:8082/api/v1/doctors/);
                }
                catch (Exception)
                {
                    var fff= "No existe Doctor";
                }
                Assert.AreEqual("1", "1");
            }
        }
    }
}
