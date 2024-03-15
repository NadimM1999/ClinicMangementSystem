using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AppointmentForCreationDto(Guid doctorId,Guid patientId,DateTimeOffset startTime, DateTimeOffset endTime, bool isCanceled);
}
