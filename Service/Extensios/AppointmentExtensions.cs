using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensios
{
    public static class AppointmentExtensions
    {
        public static Appointment ToEntity(this AppointmentForCreationDto appointmentForCreationDto) => new Appointment()
        {
            Id = Guid.NewGuid(),
            DoctorId = appointmentForCreationDto.doctorId,
            PatientId = appointmentForCreationDto.patientId,
            StartTime = appointmentForCreationDto.startTime,
            EndTime = appointmentForCreationDto.endTime,
        };

        public static AppointmentDto ToDto(this Appointment appointment)
            => new AppointmentDto(Id: appointment.Id,
            patientName: appointment.Patient.Name,
            doctorName: appointment.Doctor.Name,
            doctorId: appointment.DoctorId,
            patientId: appointment.PatientId,
            startTime: appointment.StartTime,
            endTime: appointment.EndTime,
            isCanceled: appointment.IsCanceled);

        public static IEnumerable<AppointmentDto> ToDto(this IEnumerable<Appointment> appointments)
        {
            var appointmentsDto = new List<AppointmentDto>();
            foreach (var appointment in appointments)
            {
                appointmentsDto.Add(appointment.ToDto());
            }

            return appointmentsDto;
        }
        public static AppointmentSlotsDto ToDtoSlots(this Appointment appointment)
           => new AppointmentSlotsDto(Id: appointment.Id,
           patientName: appointment.Patient.Name,
           doctorId: appointment.DoctorId,
           patientId: appointment.PatientId,
           startTime: appointment.StartTime,
           endTime: appointment.EndTime,
           isCanceled: appointment.IsCanceled);
        public static IEnumerable<AppointmentSlotsDto> ToDtoSlots(this IEnumerable<Appointment> appointments)
        {
            var appointmentsDto = new List<AppointmentSlotsDto>();
            foreach (var appointment in appointments)
            {
                appointmentsDto.Add(appointment.ToDtoSlots());
            }

            return appointmentsDto;
        }
    }
 
}
