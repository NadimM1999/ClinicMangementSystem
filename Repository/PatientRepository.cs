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
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync(PatientParameters patientParameters,bool trackChanges)=>

            await FindAll(trackChanges)
           .OrderBy(p => p.Name)
           .Skip((patientParameters.PageNumber - 1) * patientParameters.PageSize)
           .Take(patientParameters.PageSize)
           .ToListAsync();

        
        public async Task<Patient> GetPatientAsync(Guid patientId, bool trackChanges) =>
          await  FindByCondition(p =>  p.Id.Equals(patientId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreatePatint(Patient patient)=>Create(patient);

    }
}
