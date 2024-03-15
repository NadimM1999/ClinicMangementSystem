using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync(PatientParameters patientParameters ,bool trackChanges);
        Task<PatientDto> GetPatientAsync(Guid patientId , bool trackChanges);
        Task<PatientDto> CreatePatientAsync( PatientForCreationDto patient );
    }
}
