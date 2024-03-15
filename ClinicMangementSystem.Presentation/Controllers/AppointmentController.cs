using Entities.Models;
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
    [Route("api/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor, Patient, ClinicAdmin")]
    public class AppointmentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AppointmentController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments([FromQuery] AppointmentParameters appointmentParameters)
        {
            var appointments = await _serviceManager.appointmentService.GetAllAppointmentsAsync(appointmentParameters, trackChanges: false);

            return Ok(appointments);
        }

        [HttpGet("{id:guid}", Name = "appointmentById")]
        
        public async Task<IActionResult> GetAppointment(Guid id)
        {
            var appointment =await _serviceManager.appointmentService.GetAppointmentAsync(id,trackChanges:false);
            return Ok(appointment);
        }
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateAppointment(AppointmentForCreationDto appointment)
        {
            if (appointment is null)
                return BadRequest("AppointemtForCreationDto object is null.");

            var createdAppointment =await _serviceManager.appointmentService.CreateAppointmentAsync(appointment,trackChanges:false);

            return CreatedAtRoute("appointmentById", new { id = createdAppointment.Id }, createdAppointment);  
        }
        [HttpGet("id/{id:guid}")]
        public async Task<IActionResult> GetPatientAppointmentHistory(Guid id)
        {
            var appointment =await _serviceManager.appointmentService.GetPatientAppointmentHistoryAsync(id, trackChanges: false);
            return Ok(appointment);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAppointment(Guid id,[FromBody] AppointmentForUpdateDto appointment)
        {
            if (appointment is null)
                return BadRequest("AppointmentForUpdate object is null.");

          await  _serviceManager.appointmentService.UpdateAppointmentAsync(id,appointment, docTrackchanges: false, patTrackChanges: false,
                apptrackChanges: true);
            return NoContent();
        }
        [HttpPost("mostAppointment")]
        public async Task<IActionResult> MostAppointment([FromBody] DateRequestModel request, [FromQuery] DoctorParameters doctorParameters)
        {
            var appointment = await _serviceManager.appointmentService.MostAppointmentAsync(doctorParameters, trackChanges: false);

            if (request == null || request.Date == default)
            {
                return BadRequest("Invalid request body. Please provide a valid date.");
            }

            var doctorAppointmentsCounts = appointment
                .Where(appointment => appointment.startTime.Date == request.Date)
                .GroupBy(appointment => appointment.doctorId)
                .Select(group => new { DoctorId = group.Key, AppointmentCount = group.Count() })
                .OrderByDescending(x => x.AppointmentCount)
                .ToList();

            if (doctorAppointmentsCounts.Count == 0)
            {
                return NotFound("No appointments found for the specified date.");
            }

            var maxAppointmentsCount = doctorAppointmentsCounts.First().AppointmentCount;
            var doctorsWithMostAppointments = doctorAppointmentsCounts
                .Where(x => x.AppointmentCount == maxAppointmentsCount)
                .Select(x => x.DoctorId)
                .ToList();

            return Ok(doctorsWithMostAppointments);
        }
        [HttpPost("doctorsWithLongAppointments")]
        public async Task<IActionResult> GetDoctorsWithLongAppointmentsOnDate([FromBody] DateRequestModel request,[FromQuery] DoctorParameters doctorParameters)
        {
            var appointment =await _serviceManager.appointmentService.Exceeding6HoursAsync(doctorParameters,trackChanges: false);
            if (request == null || request.Date == default)
            {
                return BadRequest("Invalid request body. Please provide a valid date.");
            }

            var doctorTotalDuration = appointment
                .Where(appointment => appointment.startTime.Date == request.Date)
                .GroupBy(appointment => appointment.doctorId)
                .Select(group => new { DoctorId = group.Key, TotalDuration = group.Sum(a => (a.endTime - a.startTime).TotalHours) })
                .Where(x => x.TotalDuration >= 6)
                .ToList();

            if (doctorTotalDuration.Count == 0)
            {
                return NotFound("No doctors found with 6 or more hours of total appointments for the specified date.");
            }

            var doctorsWithLongAppointments = doctorTotalDuration
                .Select(x => x.DoctorId)
                .ToList();

            return Ok(doctorsWithLongAppointments);
        }

        [HttpDelete("{id:guid}")]
         public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            await _serviceManager.appointmentService.DeleteAppointmentAsync(id, trackChanges: false);
            return NoContent();
        }

        
    }
}
