using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicMangementSystem.Presentation.Controllers
{
    [Route("api/patients")]
    [ApiController]
    [Authorize(Roles = "Doctor, Patient, ClinicAdmin")]
    public class PatientController :ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public PatientController(IServiceManager serviceManager)=> _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetPatients([FromQuery]PatientParameters patientParameters,Guid patientId)
        {
            var patients= await _serviceManager.patientService.GetAllPatientsAsync(patientParameters,trackChanges:false);
            
            return Ok(patients);
        }

        [HttpGet("{id:guid}", Name ="PatientById")]
        public async Task<IActionResult> GetPatient( Guid id)
        {
            var patient = await _serviceManager.patientService.GetPatientAsync(id, trackChanges: false);
            return Ok(patient);
        }
        [HttpPost]
        public async Task<IActionResult> CreatrPatient([FromBody] PatientForCreationDto patient)
        {
            if (patient is null)
                return BadRequest("PatientForCreationDto is null");

            var createPatient = await _serviceManager.patientService.CreatePatientAsync(patient);
            return CreatedAtRoute("PatientById", new { id = createPatient.Id }, createPatient);
        }
    }
   
}
