using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace DisasterManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerCrisisController : ControllerBase
    {
        private readonly DisasterContext _context;

        public VolunteerCrisisController(DisasterContext context)
        {
            _context = context;
        }

        // Get all volunteer-crisis relationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolunteerCrisisDetailDto>>> GetVolunteerCrises()
        {
            var volunteerCrises = await _context.VolunteerCrises
                .Include(vc => vc.Volunteer)
                .Include(vc => vc.Crisis)
                .Select(vc => new VolunteerCrisisDetailDto
                {
                    VolunteerId = vc.VolunteerId,
                    CrisisId = vc.CrisisId,
                    Volunteer = new VolunteerDto
                    {
                        Id = vc.Volunteer.Id,
                        FullName = vc.Volunteer.FullName,
                        Age = vc.Volunteer.Age,
                        PhoneNumber = vc.Volunteer.PhoneNumber
                    },
                    Crisis = new CrisisDto
                    {
                        Id = vc.Crisis.Id,
                        Title = vc.Crisis.Title,
                        Location = vc.Crisis.Location,
                        Severity = vc.Crisis.Severity
                    }
                }).ToListAsync();

            return Ok(volunteerCrises);
        }

        // Assign volunteer to a crisis
        [HttpPost]
        public async Task<ActionResult> AssignVolunteerToCrisis([FromBody] VolunteerCrisisDto volunteerCrisisDto)
        {
            var volunteer = await _context.Volunteers.FindAsync(volunteerCrisisDto.VolunteerId);
            var crisis = await _context.Crises.FindAsync(volunteerCrisisDto.CrisisId);

            if (volunteer == null || crisis == null)
                return NotFound("Volunteer or Crisis not found");

            var volunteerCrisis = new VolunteerCrisis
            {
                VolunteerId = volunteerCrisisDto.VolunteerId,
                CrisisId = volunteerCrisisDto.CrisisId
            };

            _context.VolunteerCrises.Add(volunteerCrisis);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Remove volunteer from a crisis
        [HttpDelete("{volunteerId}/{crisisId}")]
        public async Task<IActionResult> RemoveVolunteerFromCrisis(int volunteerId, int crisisId)
        {
            var volunteerCrisis = await _context.VolunteerCrises
                .FirstOrDefaultAsync(vc => vc.VolunteerId == volunteerId && vc.CrisisId == crisisId);

            if (volunteerCrisis == null)
                return NotFound("Assignment not found");

            _context.VolunteerCrises.Remove(volunteerCrisis);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}