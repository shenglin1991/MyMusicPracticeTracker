using Microsoft.AspNetCore.Mvc;
using MusicPracticeTracker.Api.DTOs.Instruments;
using MusicPracticeTracker.Api.Services;

namespace MusicPracticeTracker.Api.Controllers;

[ApiController]
[Route("api/instruments")]
public class InstrumentsController(IInstrumentService instrumentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<InstrumentDto>>> ListAsync()
    {
        var instruments = await instrumentService.ListAsync();
        return Ok(instruments);
    }

    [HttpPost]
    public async Task<ActionResult<InstrumentDto>> CreateAsync([FromBody] CreateInstrumentRequest request)
    {
        var createdInstrument = await instrumentService.CreateAsync(request);
        return Created($"/api/instruments/{createdInstrument.Id}", createdInstrument);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<InstrumentDto>> UpdateAsync(Guid id, [FromBody] UpdateInstrumentRequest request)
    {
        var updatedInstrument = await instrumentService.UpdateAsync(id, request);
        return updatedInstrument is null ? NotFound() : Ok(updatedInstrument);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await instrumentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
