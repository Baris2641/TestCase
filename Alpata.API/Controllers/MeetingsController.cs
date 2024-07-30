using Microsoft.AspNetCore.Mvc;
using Alpata.Context;
using Alpata.Models;
using System.Linq;

namespace Alpata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MeetingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMeetings()
        {
            return Ok(_context.Meetings.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetMeeting(int id)
        {
            var meeting = _context.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        [HttpPost]
        public IActionResult CreateMeeting([FromBody] Meeting meeting)
        {
            _context.Meetings.Add(meeting);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMeeting), new { id = meeting.Id }, meeting);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMeeting(int id, [FromBody] Meeting meeting)
        {
            var existingMeeting = _context.Meetings.Find(id);
            if (existingMeeting == null)
            {
                return NotFound();
            }

            existingMeeting.Title = meeting.Title;
            existingMeeting.StartDate = meeting.StartDate;
            existingMeeting.EndDate = meeting.EndDate;
            existingMeeting.Description = meeting.Description;
            existingMeeting.Document = meeting.Document;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            var meeting = _context.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
