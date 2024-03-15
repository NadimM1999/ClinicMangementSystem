using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class PatientNotFoundException : NotFoundException
    {
        public PatientNotFoundException(Guid patientId) 
            : base($"patient with id {patientId} doesn't exsit in the database.")
        {
        }
    }
}
