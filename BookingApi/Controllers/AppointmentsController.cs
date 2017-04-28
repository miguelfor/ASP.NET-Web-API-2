using System;
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

namespace BookingApi.Controllers
{
    public class AppointmentsController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();



        private string urlDoctor = @"http://pruebas.apimedic.personalsoft.net:8082/api/v1/doctors/";
        private string urlPatient = @" http://pruebas.apimedic.personalsoft.net:8082/api/v1/patients/";
        private string validate;



        // GET: api/Appointments
        public IQueryable<Appointment> GetAppointments()
        {
            var appointmentQuery =
                    from app in db.Appointments
                    where app.status == 1
                    select app;
            return appointmentQuery;
        }

        // GET: api/Appointments/disabled
        [Route("api/Appointments/disabled")]
        [ResponseType(typeof(Appointment))]
        public IQueryable<Appointment> GetAppointmentsDisabled()
        {
            var appointmentQuery =
                    from app in db.Appointments
                    where app.status == 0
                    select app;
            return appointmentQuery;
        }

        
        // GET: api/Appointments/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // PUT: api/Appointments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.id)
            {
                return BadRequest();
            }

            //Valid if doctor and patient exists
            validate = ValidateUrl(appointment);
            if (!validate.Equals("ok"))
            {
                return BadRequest(validate);
            }
            //end



            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Appointments
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult PostAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Valid if doctor and patient exists
            validate = ValidateUrl(appointment);
            if (!validate.Equals("ok"))
            {
                return BadRequest(validate);
            }
            //end

            //Valid if this time is enabled with the doctor
            validate = ValidateTime(appointment);
            if (!validate.Equals("ok"))
            {
                return BadRequest(validate);
            }
            //end

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.id }, appointment);
        }

        // GET: api/Appointments/Avilable/5/2017-05-01
        [Route("api/Appointments/Avilable/{id}/{dateSend}")]
        [ResponseType(typeof(Date))]
        public IHttpActionResult GetAppointmentsAvilable(int id, DateTime dateSend)
        {

            Date[] dates = new Date[] {

            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 8, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 9, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 10, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 11, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 14, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 15, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 16, 0, 0)},
            new Date { idDoctor=id,  time = new DateTime(dateSend.Year, dateSend.Month, dateSend.Day, 17, 0, 0)}

       };
            foreach (var appFor in dates)
            {
                // Appointment appointmentQuery = new Appointment();
                var appointmentQuery =
                    from appQuery in db.Appointments
                    where (appQuery.date == appFor.time) && appQuery.status == 1
                    select appQuery;
                var appointmentQueryCount = appointmentQuery.Count();
                if (appointmentQueryCount > 0)
                {
                    var appointmentQueryFirst = appointmentQuery.First();
                    appFor.allNamePatient = NamePatient(appointmentQueryFirst.idPatient);
                    appFor.idAppointment = appointmentQueryFirst.id;
                }
            }
            return Ok(dates);
        }

        // DELETE: api/Appointments/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            //change Status
            appointment.status = 0;
            db.Entry(appointment).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok(appointment);
        }

       

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.id == id) > 0;
        }


        private string ValidateUrl(Appointment appointment)
        {

            var IdDoctor = appointment.idDoctor;
            var IdPatient = appointment.idPatient;
            urlDoctor += IdDoctor;
            urlPatient += IdPatient;

            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(urlDoctor);
                }
                catch (Exception)
                {
                    return "No existe Doctor";
                }
            }
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(urlPatient);
                }
                catch (Exception)
                {
                    return "No existe Patiente";
                }
            }

            return "ok";
        }

        private string ValidateTime(Appointment appointment)
        {
            int ResultIdPatient = 0;
            var appointmentQuery =
                    from app in db.Appointments
                    where app.idDoctor == appointment.idDoctor && app.date == appointment.date
                    select app;

            foreach (var app in appointmentQuery)
            {
                ResultIdPatient = app.idPatient;

            }
            var appointmentQueryCount = appointmentQuery.Count();
            if (appointmentQueryCount > 0)
            {
                string urlPatientId = urlPatient + ResultIdPatient + "/?format=json";
                using (var w = new WebClient())
                {
                    var json_data = string.Empty;
                    // attempt to download JSON data as a string
                    try
                    {
                        json_data = w.DownloadString(urlPatientId);
                        var jss = new JavaScriptSerializer();

                        var patient = jss.Deserialize<Dictionary<string, dynamic>>(json_data);
                        var patients = patient["first_name"] + " " + patient["last_name"];
                        return ("Esta hora no esta habilitada, el doctor tiene una cita asignada con " + patients);
                    }
                    catch (Exception)
                    {
                        return "No Found Patient";
                    }
                }


            }

            return "ok";
        }


        private string NamePatient(int id)
        {

            string urlPatientId = urlPatient + id + "/?format=json";
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(urlPatientId);
                    var jss = new JavaScriptSerializer();

                    var patient = jss.Deserialize<Dictionary<string, dynamic>>(json_data);
                    var patients = patient["first_name"] + " " + patient["last_name"];
                    return (patients);
                }
                catch (Exception)
                {
                    return "No Found Patient";
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}