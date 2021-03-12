using CapstoneBE.Models.Custom.Flights;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Flights
{
    public interface IFlightService
    {
        Task<int> Create();

        Task<FlightInfo> GetById(int id);

        List<FlightInfo> GetFlights();

        int GetFlightsCount();
    }
}