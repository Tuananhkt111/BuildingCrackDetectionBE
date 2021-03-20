using CapstoneBE.Data;
using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Flights
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public async Task RemoveVideo(int id)
        {
            Flight flight = await GetById(id);
            if (flight != null)
                flight.Video = null;
        }
    }
}