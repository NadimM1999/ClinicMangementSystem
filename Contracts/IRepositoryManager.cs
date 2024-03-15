using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IDoctorRepository Doctor {  get; }
        IPatientRepository Patient { get; }
        IClinicAdminRepository ClinicAdmin { get; }
        IAppointmentRepository Appointment { get; }

        Task SaveAsync();
    }
}
