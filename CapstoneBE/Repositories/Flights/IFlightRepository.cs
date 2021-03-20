using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Flights
{
    public interface IFlightRepository
    {
        Task RemoveVideo(int id);
    }
}