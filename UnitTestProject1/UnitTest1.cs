using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;  
using System.Net; 
using System.Web.Script.Serialization;
using BookingApi.Controllers;
using BookingApi.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestApiPatient()
        {
          
            using (var w = new WebClient())
            {
                //Arrange
                var patients="";
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                //Act
                try
                {
                    json_data = w.DownloadString(@"http://pruebas.apimedic.personalsoft.net:8082/api/v1/patients/13/");
                    var jss = new JavaScriptSerializer();

                    var patient = jss.Deserialize<Dictionary<string, dynamic>>(json_data);
                    patients = patient["first_name"];
                }
                catch (Exception)
                {
                    patients = "Error";
                }
                //Assert
                Assert.AreEqual("Rosa", patients );
            }
        }

        [TestMethod]
        public void TestApiDoctor()
        {

            using (var w = new WebClient())
            {
                //Arrange
                var doctors = "";
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                //Act
                try
                {
                    json_data = w.DownloadString(@"http://pruebas.apimedic.personalsoft.net:8082/api/v1/doctors/3");
                    var jss = new JavaScriptSerializer();

                    var doctor = jss.Deserialize<Dictionary<string, dynamic>>(json_data);
                    doctors = doctor["first_name"];
                }
                catch (Exception)
                {
                    doctors = "Error";
                }
                //Assert
                Assert.AreEqual("Viviana", doctors);
            }
        }

        [TestMethod]
        public void TestApiSpecialities()
        {

            using (var w = new WebClient())
            {
                //Arrange
                var specialEnd = "";
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                //Act
                try
                {
                     json_data = w.DownloadString(@"http://pruebas.apimedic.personalsoft.net:8082/api/v1/specialties/7/");
                    var jss = new JavaScriptSerializer();

                    var special = jss.Deserialize<Dictionary<string, dynamic>>(json_data);
                    specialEnd = special["specialty_type"];
                }
                catch (Exception)
                {
                    specialEnd = "Error";
                }
                //Assert
                Assert.AreEqual("Bioenergy", specialEnd);
            }
        }

        [TestMethod]
        public void TestApiAppointment()
        {
            AppointmentsController app = new AppointmentsController();
            var miguel = app.GetAppointments();
            //Assert
            Assert.AreEqual(GetTestAppointments(), miguel);
           
        }
        

    private List<Appointment> GetTestAppointments()
    {
        var test = new List<Appointment>();
        test.Add(new Appointment { id = 1, idDoctor = 8, idPatient = 13, date = new DateTime(2017, 5, 1, 8, 0, 0), status = 1 });


        return test;
    }
}
}
