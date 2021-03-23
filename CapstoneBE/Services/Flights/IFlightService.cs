using CapstoneBE.Models.Custom.Flights;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Flights
{
    public interface IFlightService
    {
        Task<FlightBasicInfo> Create(FlightCreate flightCreate);

        Task<bool> RemoveVideo(int id);

        Task<FlightInfo> GetById(int id);

        List<FlightInfo> GetFlights();

        int GetFlightsCount();
    }
}