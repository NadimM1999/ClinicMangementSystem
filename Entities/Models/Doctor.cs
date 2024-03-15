using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Doctor name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        public bool Available { get; set; }
        public int WorkHours { get; set; }
        public int AppointmentDuration {  get; set; }
        public ICollection<Patient>? Patients { get; set; } = new List<Patient>();
        public ICollection<Appointment> Appointments { get; set;} = new List<Appointment>();
    }
}
