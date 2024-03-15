using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ClinicAdmin
    {
        public string Name { get; set; } = null!;
        public Guid Id { get; set; }
        public bool CancelAppointments {  get; set; }

        public ICollection<Doctor>? DoctorsAppointmentsOnTheDayAndExceedingSixHours {  get; set; }
    }
}
