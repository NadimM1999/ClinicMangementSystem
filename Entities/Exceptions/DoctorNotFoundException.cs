using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DoctorNotFoundException : NotFoundException
    {
        public DoctorNotFoundException(Guid doctorId) 
            :base($"the Doctor with Id: {doctorId} doesn't exsist in the database.")
        {
        }
    }
}
