using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Contracts
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(AppointmentParameters appointmentParameters, bool trackChanges);
        Task<Appointment> GetAppointmentAsync(Guid appointmentId, bool trackChanges);
        Task<IEnumerable<Appointment>> GetPatientAppointmentHistoryAsync(Guid patientId,bool trackChanges);
        Task<IEnumerable<Appointment>> GetDoctorSlotsAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Appointment>> GetDoctorSlotsWithoutPatientNameAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<Appointment>> MostAppointmentAsync(bool trackChanges);
        Task<IEnumerable<Appointment>> Exceeding6HoursAsync(bool trackChanges);
        void CreateAppointment( Appointment appointment);
        void DeleteAppointment( Appointment appointment );
    }
}
