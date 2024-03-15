using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(DoctorParameters doctorParameters,bool trackChanges);
        Task<DoctorDto> GetDoctorAsync(Guid doctorId, bool trackChanges);
        Task<DoctorDto> CreateDoctorAsync(DoctorForCreationDto doctor);
        Task<IEnumerable<DoctorDto>> AvailableDoctorsAsync(bool trackChanges);

    }
}
