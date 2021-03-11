using CapstoneBE.Data;
using CapstoneBE.Models;

namespace CapstoneBE.Repositories.Flights
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }
    }
}