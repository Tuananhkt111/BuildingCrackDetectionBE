using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Cracks
{
    public interface ICrackService
    {
        Task<int> Create(CrackCreate crackCreate);

        Task<bool> Delete(int id);

        Task<bool> CreateRange(CrackCreate[] crackCreates);

        Task<bool> DeleteRange(int[] ids);

        Task<bool> Update(CrackBasicInfo crackBasicInfo, int id);

        Task<CrackInfo> GetById(int id);

        List<CrackInfo> GetCracks(string status);

        List<CrackInfo> GetCracksIgnore(string status);

        List<CrackInfo> GetCracks();

        int GetCracksCount();
        int GetCracksCountByStatus(string status, int period, int year, int[] locationIds);

        string GetMostCracksLocation(int period, int year);

        List<ChartValue> GetCracksCountBySeverity(int period, int year, int[] locationIds);
        List<ChartValue> GetCracksAssessmentCount(int period, int year, int[] locationIds);
    }
}