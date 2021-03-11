using CapstoneBE.Models.Custom.Flights;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Flights
{
    public interface IFlightService
    {
        Task<bool> Create(string video);

        Task<FlightInfo> GetById(int id);

        List<FlightInfo> GetFlights();

        int GetFlightsCount();
    }
}