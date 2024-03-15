using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IDoctorService doctorService { get; }
        IPatientService patientService { get; }
        IAppointmentService appointmentService { get; }
        IClinicAdminService clinicAdminService { get; }
        IAuthenticationService AuthenticationService { get; }

    }
}
