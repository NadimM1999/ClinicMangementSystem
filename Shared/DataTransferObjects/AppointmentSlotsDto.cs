using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AppointmentSlotsDto(Guid Id, Guid doctorId, string patientName, Guid patientId, DateTimeOffset startTime, DateTimeOffset endTime, bool isCanceled);

}
