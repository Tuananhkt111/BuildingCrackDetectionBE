using CapstoneBE.Data;
using CapstoneBE.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Cracks
{
    public class CrackRepository : GenericRepository<Crack>, ICrackRepository
    {
        public CrackRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public void CreateRange(Crack[] cracks)
        {
            _dbSet.AddRange(cracks);
        }

        public async Task Delete(int id)
        {
            Crack crack = await GetById(id);
            if (crack != null)
                _dbSet.Remove(crack);
        }

        public void DeleteRange(int[] ids)
        {
            Crack[] cracks = Get(filter: c => ids.Contains(c.CrackId)).ToArray();
            if (cracks != null && cracks.Length > 0)
                _dbSet.RemoveRange(cracks);
        }
    }
}