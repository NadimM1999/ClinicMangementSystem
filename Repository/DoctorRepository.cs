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
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async  Task<IEnumerable<Doctor>> GetAllDoctorsAsync(DoctorParameters doctorParameters,bool trackChanges)=>

           await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .Skip((doctorParameters.PageNumber - 1) * doctorParameters.PageSize)
            .Take(doctorParameters.PageSize)
            .ToListAsync();
            



        public async Task<Doctor> GetDoctorAsync(Guid doctorId, bool trackChanges) =>
           await FindByCondition(c => c.Id.Equals(doctorId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateDoctor(Doctor doctor)=> Create(doctor);

        public async Task<IEnumerable<Doctor>> AvailableDoctorsAsync(bool trackChanges) =>
           await FindAll(trackChanges)
            .OrderBy(c => c.Available)
            .ToListAsync();



    }
}
