using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Cracks
{
    public interface ICrackRepository
    {
        Task Delete(int id);

        void CreateRange(Crack[] cracks);

        void DeleteRange(int[] ids);
    }
}