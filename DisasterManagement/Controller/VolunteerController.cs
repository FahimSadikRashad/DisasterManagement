using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace DisasterManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly DisasterContext _context;

        public VolunteerController(DisasterContext context)
        {
            _context = context;
        }

        // Get all volunteers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolunteerDto>>> GetVolunteers()
        {
            var volunteers = await _context.Volunteers
                .Select(v => new VolunteerDto
                {
                    Id = v.Id,
                    FullName = v.FullName,
                    Age = v.Age,
                    PhoneNumber = v.PhoneNumber,
                    AssignedTask = v.AssignedTask
                }).ToListAsync();

            return Ok(volunteers);
        }

        // Get a single volunteer by id
        [HttpGet("{id}")]
        public async Task<ActionResult<VolunteerDto>> GetVolunteer(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
                return NotFound("Volunteer not found");

            var volunteerDto = new VolunteerDto
            {
                Id = volunteer.Id,
                FullName = volunteer.FullName,
                Age = volunteer.Age,
                PhoneNumber = volunteer.PhoneNumber,
                AssignedTask = volunteer.AssignedTask
            };

            return Ok(volunteerDto);
        }

        // Create a new volunteer
        [HttpPost]
        public async Task<ActionResult<VolunteerDto>> CreateVolunteer([FromBody] VolunteerCreateDto volunteerCreateDto)
        {
            var volunteer = new Volunteer
            {
                FullName = volunteerCreateDto.FullName,
                Age = volunteerCreateDto.Age,
                PhoneNumber = volunteerCreateDto.PhoneNumber,
                AssignedTask = volunteerCreateDto.AssignedTask
            };

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            var volunteerDto = new VolunteerDto
            {
                Id = volunteer.Id,
                FullName = volunteer.FullName,
                Age = volunteer.Age,
                PhoneNumber = volunteer.PhoneNumber,
                AssignedTask = volunteer.AssignedTask
            };

            return CreatedAtAction(nameof(GetVolunteer), new { id = volunteer.Id }, volunteerDto);
        }

        // Update a volunteer
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVolunteer(int id, [FromBody] VolunteerCreateDto volunteerUpdateDto)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
                return NotFound("Volunteer not found");

            volunteer.FullName = volunteerUpdateDto.FullName;
            volunteer.Age = volunteerUpdateDto.Age;
            volunteer.PhoneNumber = volunteerUpdateDto.PhoneNumber;
            volunteer.AssignedTask = volunteerUpdateDto.AssignedTask;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a volunteer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteer(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
                return NotFound("Volunteer not found");

            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}