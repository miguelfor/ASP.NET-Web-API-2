using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace BookingApi.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Doctor is Required")]
        public int idDoctor { get; set; }
        [Required(ErrorMessage = "Patient is Required")]
        public int idPatient { get; set; }
        [Required(ErrorMessage = "Date is Required")] 
        public DateTime date { get; set; }
        int value = 1;
        [DefaultValue(1)]
        public int status
        {
            get { return this.value; }

            set { this.value = value; }
        }
    }
}