using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(DoctorParameters doctorParameters,bool trackChanges);
        Task<Doctor> GetDoctorAsync(Guid doctorId, bool trackChanges);
        void CreateDoctor(Doctor doctor);
        Task<IEnumerable<Doctor>> AvailableDoctorsAsync(bool trackChanges);
        
    }
}
