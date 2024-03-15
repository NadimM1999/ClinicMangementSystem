using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
       
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IDoctorRepository> _doctorRepository;
        private readonly Lazy<IPatientRepository> _patientRepository;
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;
        private readonly Lazy<IClinicAdminRepository> _clinicAdminRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _doctorRepository = new Lazy<IDoctorRepository> (()=>
                new DoctorRepository(repositoryContext));
            _patientRepository = new Lazy<IPatientRepository> (()=> 
                new PatientRepository(repositoryContext));
            _clinicAdminRepository = new Lazy<IClinicAdminRepository> (()=> 
                new ClinicAdminRepository(repositoryContext));
            _appointmentRepository = new Lazy<IAppointmentRepository>(() =>
                new AppointmentRepository(repositoryContext));
        }
        public IDoctorRepository Doctor => _doctorRepository.Value;

        public IPatientRepository Patient => _patientRepository.Value;

        public IClinicAdminRepository ClinicAdmin => _clinicAdminRepository.Value;

        public IAppointmentRepository Appointment => _appointmentRepository.Value;

        public async Task SaveAsync() =>await _repositoryContext.SaveChangesAsync();
    }
}
