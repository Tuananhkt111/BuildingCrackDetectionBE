using CapstoneBE.Models.Custom.Cracks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Cracks
{
    public interface ICrackService
    {
        Task<bool> Create(CrackCreate crackCreate);

        Task<bool> Delete(int id);

        Task<bool> CreateRange(CrackCreate[] crackCreates);

        Task<bool> DeleteRange(int[] ids);

        Task<bool> Update(CrackBasicInfo crackBasicInfo, int id);

        Task<CrackInfo> GetById(int id);

        List<CrackInfo> GetCracks(string status);

        List<CrackInfo> GetCracksIgnore(string status);

        List<CrackInfo> GetCracks();

        int GetCracksCount();
    }
}