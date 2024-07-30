using Alpata.Context;
using Alpata.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpata.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly AppDbContext _context;

        public MeetingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Meeting> CreateMeeting(Meeting meeting)
        {
            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();
            return meeting;
        }

        public async Task<Meeting> GetMeeting(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                throw new KeyNotFoundException("Meeting not found.");
            }
            return meeting;
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetings()
        {
            return await _context.Meetings.ToListAsync();
        }

        public async Task<Meeting> UpdateMeeting(Meeting meeting)
        {
            var existingMeeting = await _context.Meetings.FindAsync(meeting.Id);
            if (existingMeeting == null)
            {
                throw new KeyNotFoundException("Meeting not found.");
            }

            _context.Entry(meeting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return meeting;
        }

        public async Task<bool> DeleteMeeting(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return false;
            }

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
