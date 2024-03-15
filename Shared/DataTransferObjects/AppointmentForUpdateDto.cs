using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AppointmentForUpdateDto(Guid doctorId, Guid patientId, string doctorName, string patientName, DateTimeOffset startTime, DateTimeOffset endTime, bool isCanceledd);

}
