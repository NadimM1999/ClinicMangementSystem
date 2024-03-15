using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDoctorService> _doctorService;
        private readonly Lazy<IPatientService> _patientService;
        private readonly Lazy<IAppointmentService> _appointmentService;
        private readonly Lazy<IClinicAdminService> _clinicalAdminService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager ,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _doctorService = new Lazy<IDoctorService>(()=>
                new DoctorService(repositoryManager, loggerManager, mapper, _appointmentService.Value));
           
            _patientService = new Lazy<IPatientService>(()=> 
                new PatientService(repositoryManager,loggerManager, mapper));
            _clinicalAdminService = new Lazy<IClinicAdminService>(()=>
                new CLinicAdminService(repositoryManager, loggerManager, mapper));
            _appointmentService = new Lazy<IAppointmentService>(() =>
                new AppointmentService(repositoryManager, loggerManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(loggerManager, mapper, userManager,
            configuration));

        }
    public IDoctorService doctorService => _doctorService.Value;

        public IPatientService patientService => _patientService.Value;

        public IAppointmentService appointmentService => _appointmentService.Value;

        public IClinicAdminService clinicAdminService => _clinicalAdminService.Value;
        public IAuthenticationService AuthenticationService =>
            _authenticationService.Value;
    }
}
