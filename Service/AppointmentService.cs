using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Service.Extensios;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class AppointmentService : IAppointmentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public AppointmentService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(AppointmentParameters appointmentParameters,bool trackChanges)
        {
            var appointmentsWithMetaData = await _repositoryManager.Appointment.GetAllAppointmentsAsync(appointmentParameters,trackChanges);
            
            return appointmentsWithMetaData.ToDto();
        }

        public async Task<AppointmentDto> GetAppointmentAsync(Guid id, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentAsync(id, trackChanges);
            
            if (appointment is null)
                throw new AppointmentNotFoundException(id);
            
            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            return appointmentDto;
        }

        public async Task<AppointmentDto> CreateAppointmentAsync( AppointmentForCreationDto appointmentCreateReq,bool trackChanges)
        {
            
            var appointmentEntity = appointmentCreateReq.ToEntity();
            
            _repositoryManager.Appointment.CreateAppointment(appointmentEntity);
            
            await _repositoryManager.SaveAsync();

            var createdAppointment = await _repositoryManager.Appointment.GetAppointmentAsync(appointmentEntity.Id, trackChanges);
            
            var appointmentToReturn =  createdAppointment.ToDto();


            return appointmentToReturn;
        }

        public async Task<IEnumerable<AppointmentDto>> GetPatientAppointmentHistoryAsync(Guid id, bool trackChanges)
        {
            var patient = await _repositoryManager.Patient.GetPatientAsync(id, trackChanges);
            if (patient is null)
                throw new PatientNotFoundException(id);
            var appointment =await _repositoryManager.Appointment.GetPatientAppointmentHistoryAsync(id, trackChanges);
            var appointmentToReturn = _mapper.Map<IEnumerable<AppointmentDto>>(appointment);
            return appointmentToReturn;
        }


        public async Task UpdateAppointmentAsync(Guid id,AppointmentForUpdateDto appointment, bool docTrackchanges, bool patTrackChanges, bool apptrackChanges)
        {
            var doctor = await _repositoryManager.Doctor.GetDoctorAsync(id,docTrackchanges);
            var patient = await _repositoryManager.Patient.GetPatientAsync(id, patTrackChanges);
            var appointmentEntity = await _repositoryManager.Appointment.GetAppointmentAsync(id, apptrackChanges);
            if(appointmentEntity is null )
                throw new AppointmentNotFoundException(id);

            _mapper.Map(appointment, appointmentEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<AppointmentDto>> MostAppointmentAsync(DoctorParameters doctorParameters, bool trackChanges)
        {
            var doctor = await _repositoryManager.Doctor.GetAllDoctorsAsync(doctorParameters,trackChanges);
            var appointment = await _repositoryManager.Appointment.MostAppointmentAsync(trackChanges);
            var appointmentDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointment);
            return appointmentDto;
        }

        public async Task DeleteAppointmentAsync(Guid id, bool trackChanges)
        {
           
           var appointment = await _repositoryManager.Appointment.GetAppointmentAsync(id, trackChanges);
            _repositoryManager.Appointment.DeleteAppointment(appointment);
            _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<AppointmentDto>> Exceeding6HoursAsync(DoctorParameters doctorParameters, bool trackChanges)
        {
            var doctor = await _repositoryManager.Doctor.GetAllDoctorsAsync(doctorParameters,trackChanges);
            var appointment = await _repositoryManager.Appointment.Exceeding6HoursAsync(trackChanges);
            var appointmentDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointment);

            return appointmentDto;
        }
        public async Task<IEnumerable<AppointmentDto>> GetDoctorSlotsAsync(Guid doctorId, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetDoctorSlotsAsync(doctorId, trackChanges: false);
            var appoitmentDto = appointments.ToDto();

            return appoitmentDto;
        }

        public async Task<IEnumerable<AppointmentSlotsDto>> GetDoctorSlotsWithoutPatientNameAsync(Guid doctorId, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetDoctorSlotsAsync(doctorId, trackChanges: false);
            var appoitmentDto = appointments.ToDtoSlots();

            return appoitmentDto;
        }
    }
}
