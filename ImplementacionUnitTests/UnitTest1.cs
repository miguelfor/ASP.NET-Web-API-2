using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookingApi.Controllers;
using BookingApi.Models;
using Flurl.Http;
using System.Net.Http;
using System.Net;
using System.Text;  // for class Encoding  
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

namespace ImplementacionUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*Arrange o plantemaniento
            */
            private ApplicationDbContext db = new ApplicationDbContext();
            AppointmentsController appControler = new AppointmentsController();
            Appointment appModel = new Appointment();
            //Act o Prueba
            //var recibe = appControler.GetAppointments();
            var appointmentQuery =
                   from app in   db.Appointments
                   where app.status == 1
                   select app;
            //Assert O Afirmacion
            Assert.AreEqual("aa","aa");




        }
    }
}
