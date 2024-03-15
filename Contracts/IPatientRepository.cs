using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>>  GetAllPatientsAsync(PatientParameters patientParameters,bool trackChanges);
        Task<Patient> GetPatientAsync(Guid patientId ,bool trackChanges);
        void CreatePatint(Patient patient);
    }
}
