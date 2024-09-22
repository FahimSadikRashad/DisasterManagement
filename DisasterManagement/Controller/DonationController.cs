using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;


[ApiController]
[Route("api/[controller]")]
public class DonationController : ControllerBase
{
    private readonly DisasterContext _context;

    public DonationController(DisasterContext context)
    {
        _context = context;
    }

    // Get all donations (Response: 200 OK)
    [HttpGet]
    public ActionResult<IEnumerable<DonationDto>> GetDonations()
    {
        var donations = _context.Donations
            .Select(d => new DonationDto
            {
                Id = d.Id,
                Amount = d.Amount,
                Date = d.Date
            }).ToList();

        return Ok(donations);
    }

    // Get a single donation by id (Response: 200 OK or 404 Not Found)
    [HttpGet("{id}")]
    public ActionResult<DonationDto> GetDonation(int id)
    {
        var donation = _context.Donations.FirstOrDefault(d => d.Id == id);

        if (donation == null)
            return NotFound("Donation not found");

        var donationDto = new DonationDto
        {
            Id = donation.Id,
            Amount = donation.Amount,
            Date = donation.Date
        };

        return Ok(donationDto);
    }

    // Create a new donation (Response: 201 Created or 400 Bad Request)
    [HttpPost]
    public ActionResult<DonationDto> CreateDonation(DonationCreateDto donationCreateDto)
    {
        var donation = new Donation
        {
            Amount = donationCreateDto.Amount,
            Date = DateTime.Now
        };

        _context.Donations.Add(donation);
        _context.SaveChanges();

        var donationDto = new DonationDto
        {
            Id = donation.Id,
            Amount = donation.Amount,
            Date = donation.Date
        };

        return CreatedAtAction(nameof(GetDonation), new { id = donation.Id }, donationDto);
    }

    // Delete a donation (Response: 204 No Content or 404 Not Found)
    [HttpDelete("{id}")]
    public IActionResult DeleteDonation(int id)
    {
        var donation = _context.Donations.FirstOrDefault(d => d.Id == id);

        if (donation == null)
            return NotFound("Donation not found");

        _context.Donations.Remove(donation);
        _context.SaveChanges();

        return NoContent();
    }
}
