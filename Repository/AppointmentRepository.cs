using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(AppointmentParameters appointmentParameters,bool trackChanges)
        {
          var appointment =   await FindAll(trackChanges)
           .Include(a=> a.Doctor)
           .Include(a=> a.Patient)
           .OrderBy(a => a.StartTime)
           .Skip((appointmentParameters.PageNumber - 1) * appointmentParameters.PageSize)
           .Take(appointmentParameters.PageSize)
           .ToListAsync();
            return appointment;
        }

        public async Task<Appointment> GetAppointmentAsync(Guid Id, bool trackChanges)
        {
            var appointment = await FindByCondition(a => a.Id.Equals(Id), trackChanges)
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .SingleOrDefaultAsync();

            return appointment;
        }
         

        public void CreateAppointment(Appointment appointment)=>
            Create(appointment);

        public async Task<IEnumerable<Appointment>> GetPatientAppointmentHistoryAsync(Guid patientId, bool trackChanges)=>
           await FindByCondition(p => p.PatientId.Equals(patientId), trackChanges)
             .OrderBy(a=> a.StartTime) 
             .ToListAsync();

        public async Task<IEnumerable<Appointment>> MostAppointmentAsync(bool trackChanges)=>
           await FindAll(trackChanges)
            .OrderBy(a=> a.DoctorId)
            .ToListAsync();

        public void DeleteAppointment(Appointment appointment)=>Delete(appointment);

        public async Task<IEnumerable<Appointment>> Exceeding6HoursAsync(bool trackChanges)=>
           await FindAll(trackChanges)
            .OrderBy (a=> a.DoctorId)
            .ToListAsync();

        public async Task<IEnumerable<Appointment>> GetDoctorSlotsAsync(Guid doctorId, bool trackChanges)
        {
            return await FindByCondition(a => a.DoctorId.Equals(doctorId), trackChanges)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetDoctorSlotsWithoutPatientNameAsync(Guid doctorId, bool trackChanges)
        {
            return await FindByCondition(a => a.DoctorId.Equals(doctorId), trackChanges)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }
    }
}