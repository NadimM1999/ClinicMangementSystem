using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class PatientService : IPatientService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public PatientService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync(PatientParameters patientParameters,bool trackChanges)
        {
            var patientsWithMetaData = await _repositoryManager.Patient.GetAllPatientsAsync(patientParameters,trackChanges);
            var patientDto = _mapper.Map<IEnumerable<PatientDto>>(patientsWithMetaData);
            return patientDto;
        }

        public async Task<PatientDto> GetPatientAsync(Guid patientid, bool trackChanges)
        {
            var patientDb = await _repositoryManager.Patient.GetPatientAsync(patientid, trackChanges);
            if (patientDb is null)
                throw new PatientNotFoundException(patientid);
            var patient = _mapper.Map<PatientDto>(patientDb);

            return patient;

        }

        public async Task<PatientDto> CreatePatientAsync(PatientForCreationDto patient)
        {
            var employeeEntite = _mapper.Map<Patient>(patient);
            _repositoryManager.Patient.CreatePatint(employeeEntite);
           await _repositoryManager.SaveAsync();

            var PatientToRetuen = _mapper.Map<PatientDto>(employeeEntite);
            return PatientToRetuen;
        }
    }
}
