using CapstoneBE.Repositories.User;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace CapstoneBE.UnitOfWorks
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }

        Task<int> Save();

        IDbContextTransaction GetTransaction();
    }
}