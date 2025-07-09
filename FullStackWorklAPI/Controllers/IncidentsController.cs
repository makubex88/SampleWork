using FullStackWorkAPI.Exceptions;
using FullStackWorkAPI.Interfaces;
using FullStackWorkAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FullStackWorkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentsController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        /// <summary>
        /// Creates a new incident with validation and duplicate detection
        /// </summary>
        /// <param name="request">The incident creation request</param>
        /// <returns>The created incident or validation errors</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Incident), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentRequest request)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Validation failed",
                    Details = ModelState.SelectMany(x => x.Value.Errors)
                        .Select(e => e.ErrorMessage).ToList()
                });
            }

            try
            {
                var incident = await _incidentService.CreateIncidentAsync(request);
                return CreatedAtAction(nameof(GetIncident), new { id = incident.Id }, incident);
            }
            catch (DuplicateIncidentException ex)
            {
                return Conflict(new ErrorResponse
                {
                    Error = "Duplicate incident detected",
                    Details = new List<string> { ex.Message }
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Invalid input",
                    Details = new List<string> { ex.Message }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "Internal server error",
                    Details = new List<string> { "An unexpected error occurred" }
                });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Incident), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIncident(int id)
        {
            var incident = await _incidentService.GetIncidentAsync(id);
            return incident != null ? Ok(incident) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(Incident), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllIncident()
        {
            var incidents = await _incidentService.GetAllIncidentsAsync();
            return Ok(incidents);
        }
    }
}
