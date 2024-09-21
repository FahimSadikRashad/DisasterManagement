using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly DisasterContext _context;

    public InventoryController(DisasterContext context)
    {
        _context = context;
    }

    // Get all inventory items (Response: 200 OK)
    [HttpGet]
    public ActionResult<IEnumerable<InventoryDto>> GetInventory()
    {
        var inventory = _context.Inventories
            .Select(i => new InventoryDto
            {
                Id = i.Id,
                Name = i.Name,
                Type = i.Type,
                Quantity = i.Quantity
            }).ToList();

        return Ok(inventory);
    }

    // Get a single inventory item by id (Response: 200 OK or 404 Not Found)
    [HttpGet("{id}")]
    public ActionResult<InventoryDto> GetInventoryItem(int id)
    {
        var item = _context.Inventories.FirstOrDefault(i => i.Id == id);

        if (item == null)
            return NotFound("Inventory item not found");

        var inventoryDto = new InventoryDto
        {
            Id = item.Id,
            Name = item.Name,
            Type = item.Type,
            Quantity = item.Quantity
        };

        return Ok(inventoryDto);
    }

    // Create a new inventory item (Response: 201 Created or 400 Bad Request)
    [HttpPost]
    public ActionResult<InventoryDto> CreateInventoryItem(InventoryCreateDto inventoryCreateDto)
    {
        var item = new Inventory
        {
            Name = inventoryCreateDto.Name,
            Type = inventoryCreateDto.Type,
            Quantity = inventoryCreateDto.Quantity
        };

        _context.Inventories.Add(item);
        _context.SaveChanges();

        var inventoryDto = new InventoryDto
        {
            Id = item.Id,
            Name = item.Name,
            Type = item.Type,
            Quantity = item.Quantity
        };

        return CreatedAtAction(nameof(GetInventoryItem), new { id = item.Id }, inventoryDto);
    }

    // Update an inventory item (Response: 204 No Content or 404 Not Found)
    [HttpPut("{id}")]
    public IActionResult UpdateInventoryItem(int id, InventoryCreateDto inventoryUpdateDto)
    {
        var item = _context.Inventories.FirstOrDefault(i => i.Id == id);

        if (item == null)
            return NotFound("Inventory item not found");

        item.Name = inventoryUpdateDto.Name;
        item.Type = inventoryUpdateDto.Type;
        item.Quantity = inventoryUpdateDto.Quantity;

        _context.SaveChanges();

        return NoContent();
    }

    // Delete an inventory item (Response: 204 No Content or 404 Not Found)
    [HttpDelete("{id}")]
    public IActionResult DeleteInventoryItem(int id)
    {
        var item = _context.Inventories.FirstOrDefault(i => i.Id == id);

        if (item == null)
            return NotFound("Inventory item not found");

        _context.Inventories.Remove(item);
        _context.SaveChanges();

        return NoContent();
    }
}
