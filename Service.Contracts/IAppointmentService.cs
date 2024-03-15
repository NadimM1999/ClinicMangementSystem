using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>>GetAllAppointmentsAsync(AppointmentParameters appointmentParameters, bool trackChanges);
        Task<AppointmentDto> GetAppointmentAsync(Guid id,bool trackChanges);
        Task<AppointmentDto> CreateAppointmentAsync( AppointmentForCreationDto appointment, bool trackChanges);
        Task<IEnumerable<AppointmentDto>> GetPatientAppointmentHistoryAsync(Guid id, bool trackChanges);
        Task<IEnumerable<AppointmentSlotsDto>> GetDoctorSlotsWithoutPatientNameAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<AppointmentDto>> GetDoctorSlotsAsync(Guid doctorId, bool trackChanges);
        Task<IEnumerable<AppointmentDto>> MostAppointmentAsync(DoctorParameters doctorParameters,bool trackChanges);        
        Task<IEnumerable<AppointmentDto>> Exceeding6HoursAsync(DoctorParameters doctorParameters, bool trackChanges);
        Task UpdateAppointmentAsync(Guid id,AppointmentForUpdateDto appointment,bool docTrackchanges,bool patTrackChanges ,bool apptrackChanges);
        Task DeleteAppointmentAsync(Guid id, bool trackChanges);
        
    }
}
