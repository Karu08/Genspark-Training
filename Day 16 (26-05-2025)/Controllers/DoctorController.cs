using Microsoft.AspNetCore.Mvc;
using FIRSTAPI.Models;
using System.Collections.Generic;

namespace FIRSTAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DoctorController : ControllerBase
    {
        static List<Doctor> doctors = new List<Doctor>
    {
        new Doctor{Id=101,Name="Ramu",Speciality="Pediatrics"},
        new Doctor{Id=102,Name="Somu",Speciality="Cardiology"},
    };
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return Ok(doctors);
        }
        [HttpPost]
        public ActionResult<Doctor> PostDoctor([FromBody] Doctor doctor)
        {
            doctors.Add(doctor);
            return Created("", doctor);
        }

        [HttpPut("{id}")]
        public ActionResult<Doctor> UpdateDoctor(int id, [FromBody] Doctor updatedDoctor)
        {
            var existingDoctor = doctors.FirstOrDefault(d => d.Id == id);
            if (existingDoctor == null)
                return NotFound($"Doctor with ID {id} not found.");

            existingDoctor.Name = updatedDoctor.Name;
            existingDoctor.Speciality = updatedDoctor.Speciality;

            return Ok(existingDoctor);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDoctor(int id)
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null)
                return NotFound($"Doctor with ID {id} not found.");

            doctors.Remove(doctor);
            return NoContent();
        }
    }
}