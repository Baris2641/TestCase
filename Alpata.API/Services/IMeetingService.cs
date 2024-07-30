using Alpata.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpata.Services
{
    public interface IMeetingService
    {
        Task<Meeting> CreateMeeting(Meeting meeting);
        Task<Meeting> GetMeeting(int id);
        Task<IEnumerable<Meeting>> GetAllMeetings();
        Task<Meeting> UpdateMeeting(Meeting meeting);
        Task<bool> DeleteMeeting(int id);
    }
}
