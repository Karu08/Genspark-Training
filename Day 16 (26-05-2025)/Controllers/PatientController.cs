using Microsoft.AspNetCore.Mvc;
using FIRSTAPI.Models;
using System.Collections.Generic;

namespace FIRSTAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : ControllerBase
    {
        static List<Patient> patients = new List<Patient>
    {
        new Patient{Id="P1",Name="Gita",Age=20},
        new Patient{Id="P2",Name="Aman",Age=23},
    };
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(patients);
        }
        [HttpPost]
        public ActionResult<Patient> PostPatient([FromBody] Patient patient)
        {
            patients.Add(patient);
            return Created("", patient);
        }

        [HttpPut("{id}")]
        public ActionResult<Patient> UpdatePatient(string id, [FromBody] Patient updatedPatient)
        {
            var existingPatient = patients.FirstOrDefault(d => d.Id == id);
            if (existingPatient == null)
                return NotFound($"Patient with ID {id} not found.");

            existingPatient.Name = updatedPatient.Name;
            existingPatient.Age = updatedPatient.Age;

            return Ok(existingPatient);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePatient(string id)
        {
            var patient = patients.FirstOrDefault(d => d.Id == id);
            if (patient == null)
                return NotFound($"Patient with ID {id} not found.");

            patients.Remove(patient);
            return NoContent();
        }
    }
}