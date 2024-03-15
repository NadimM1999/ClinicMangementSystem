using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class AppointmentNotFoundException : NotFoundException
    {
        public AppointmentNotFoundException(Guid appointmentId)
            : base($"the Appointment with Id: {appointmentId} doesn't exsist in the database.")
        {
        }
    }
}
