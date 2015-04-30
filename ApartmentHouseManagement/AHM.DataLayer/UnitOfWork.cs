using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;
using AHM.DataLayer.Repositories;

namespace AHM.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private IBillRepository _billRepository;

        private bool _disposed;

        private readonly Dictionary<Type, object> _repositories;
        private AhmContext _context;


        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_context));
            }
        }

        public IBillRepository BillRepository
        {
            get
            {
                return _billRepository ?? (_billRepository = new BillRepository(_context));
            }
        }


        public UnitOfWork(AhmContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _disposed = false;
        }


        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IBaseRepository<TEntity>;
            }

            BaseRepository<TEntity> repository;
            if (typeof(TEntity) == typeof(Package))
            {
                repository = new PackageRepository(_context) as BaseRepository<TEntity>;
            }
            else if (typeof (TEntity) == typeof (Occupant))
            {
                repository = new OccupantRepository(_context) as BaseRepository<TEntity>;
            }
            else if (typeof(TEntity) == typeof(User))
            {
                repository = UserRepository as BaseRepository<TEntity>;
            }
            else if (typeof(TEntity) == typeof(Bill))
            {
                repository = BillRepository as BaseRepository<TEntity>;
            }
            else if (typeof(TEntity) == typeof(UtilitiesItem))
            {
                repository = new UtilitiesItemRepository(_context) as BaseRepository<TEntity>;
            }
            else
            {
                repository = new BaseRepository<TEntity>(_context);
            }
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose();
                }

                _context = null;
                _disposed = true;
            }
        }
    }
}