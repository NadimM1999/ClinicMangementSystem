using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record DoctorForCreationDto(string Name, bool Available ,int WorkHours, int AppointmentDuration);

}
