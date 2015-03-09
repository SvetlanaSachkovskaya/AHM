using System.Threading.Tasks;

namespace AHM.DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveAsync();

        void Dispose();
    }
}