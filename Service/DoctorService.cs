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
    internal sealed class DoctorService : IDoctorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointmentService;
        public DoctorService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper,IAppointmentService appointmentService)
        { 
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _appointmentService = appointmentService;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(DoctorParameters doctorParameters,bool trackChanges)
        {
                var doctorsWithMetaData = await _repositoryManager.Doctor.GetAllDoctorsAsync(doctorParameters,trackChanges);
                var doctorsDto = _mapper.Map<IEnumerable<DoctorDto>>(doctorsWithMetaData);

            return doctorsDto;

        }

        public async Task<DoctorDto> GetDoctorAsync(Guid id, bool trackChanges)
        {
            var doctor = await _repositoryManager.Doctor.GetDoctorAsync(id, trackChanges);
            if (doctor is null)
                throw new DoctorNotFoundException(id);

            var doctorDto = _mapper.Map<DoctorDto>(doctor);

            return doctorDto;
        }
        public async Task<DoctorDto> CreateDoctorAsync(DoctorForCreationDto doctor)
        {
            var doctorEntity = _mapper.Map<Doctor>(doctor);
            _repositoryManager.Doctor.CreateDoctor(doctorEntity);
           await _repositoryManager.SaveAsync();

            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);

            return doctorToReturn;
        }

        public async Task<IEnumerable<DoctorDto>> AvailableDoctorsAsync(bool trackChanges)
        {
            var doctor =  await _repositoryManager.Doctor.AvailableDoctorsAsync(trackChanges);
            var doctorDto = _mapper.Map<IEnumerable<DoctorDto>>(doctor);
            return doctorDto;
        }

        
    }
}
