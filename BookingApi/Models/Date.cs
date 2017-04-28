using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookingApi.Models
{
    public class Date
    {
        int valueId = 0;
        public int idAppointment
        {
            get { return this.valueId; }

            set { this.valueId = value; }
        }
        public int idDoctor { get; set; } 
        public int idPatient { get; set; }
        public string allNamePatient { get; set; } 
        public DateTime time { get; set; } 
    }
}