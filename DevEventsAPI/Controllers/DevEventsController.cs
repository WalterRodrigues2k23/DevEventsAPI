using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DevEventsAPI.Entities;
using DevEventsAPI.Persistense;

namespace DevEventsAPI.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        // api/dev-events GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();
            return Ok(devEvents);
        }

        //api/dev-events/123125123 GET
        [HttpGet("{id}")]
        public IActionResult GetById(Guid Id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == Id);
            if (devEvent == null)
            {
                return NotFound();
            }
            return Ok(devEvent);
        }

        // api/dev-events POST 
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);

            return CreatedAtAction(nameof(GetById), new {id = devEvent.Id}, devEvent);
        }

        // api/dev-events/12354125 PUT
        [HttpPut("{id}")]
        public IActionResult UpDate(Guid Id, DevEvent input)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == Id);
            if (devEvent == null)
            {
                return NotFound();
            }
            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        // api/dev-events/123124124 DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id) 
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == Id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            return NoContent();
        }

        // api/dev-events/12312312312/speakers
        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid Id, DevEventSpeaker speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == Id);
            if (devEvent == null)
            {
                return NotFound();
            }
            devEvent.Speakers.Add(speaker);

            return NoContent();
        }
    }
}
