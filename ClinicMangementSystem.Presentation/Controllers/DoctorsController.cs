using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestFeatures;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace ClinicMangementSystem.Presentation.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    [Authorize(Roles = "Doctor, Patient, ClinicAdmin")]
    public class DoctorsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public DoctorsController(IServiceManager serviceManager)=> _serviceManager = serviceManager;
        
        [HttpGet]
        public async Task<IActionResult> GetDoctors([FromQuery] DoctorParameters doctorParameters)
        {
            var doctors = await _serviceManager.doctorService.GetAllDoctorsAsync(doctorParameters,trackChanges: false);
            var doctorDto = doctors.Select(d => new
            {
                Name = d.Name,
                Id = d.Id,
            });

            return Ok(doctors);
        }

        [HttpGet("{id:guid}", Name = "DoctorById")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
           var doctor =await _serviceManager.doctorService.GetDoctorAsync(id, trackChanges: false);

            return Ok(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorForCreationDto doctor)
        {
            if (doctor is null)
                return BadRequest("Doctor for creationDto is null.");

            var createdDcotor =await _serviceManager.doctorService.CreateDoctorAsync(doctor);

            return CreatedAtRoute("DoctorById", new { id = createdDcotor.Id }, createdDcotor);
        }
        [HttpGet("{availability}")]
        public async Task<IActionResult> AvailableDoctors()
        {
            var availbleDoctors =await _serviceManager.doctorService.AvailableDoctorsAsync(trackChanges: false);
            var doctorInfo = availbleDoctors.Select(d => new
            {
                Name = d.Name,
                Available = d.Available
            }).ToList();

            return Ok(doctorInfo);
        }

        [HttpGet("{doctorId}/slots")]
        public async Task<IActionResult> GetDoctorSlots(Guid doctorId)
        {
            bool includePatientName = User.IsInRole("Doctor");

            if(includePatientName)
            {
                var doctorAppointments = await _serviceManager.appointmentService.GetDoctorSlotsAsync(doctorId, includePatientName);

                return Ok(doctorAppointments);
            }
            else
            {
                var doctorAppointment = await _serviceManager.appointmentService.GetDoctorSlotsWithoutPatientNameAsync(doctorId, includePatientName);
                return Ok(doctorAppointment);
            }
            
        }
    }

}

