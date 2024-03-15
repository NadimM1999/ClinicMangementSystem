using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset EndTime { get; set; } 
        public bool IsCanceled { get; set; }
        public Guid DoctorId {  get; set; }
        public Doctor Doctor { get; set; }  = null!;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
