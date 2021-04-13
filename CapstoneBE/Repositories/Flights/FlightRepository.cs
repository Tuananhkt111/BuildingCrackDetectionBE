using CapstoneBE.Data;
using CapstoneBE.Models;
using System;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Flights
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public void RemoveVideo(Flight flight)
        {
            flight.Video = null;
            flight.DeleteVideoDate = DateTime.UtcNow;
        }
    }
}