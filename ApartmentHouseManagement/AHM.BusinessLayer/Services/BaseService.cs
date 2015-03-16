using System;
using System.Threading.Tasks;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;


        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        protected async Task<ModifyDbStateResult> AddEntityAsync<TEntity>(TEntity entity, string errorMessage, Func<Task> action) where TEntity: Entity
        {
            var validationResult = entity.Validate();
            if (!validationResult.IsValid)
            {
                return new ModifyDbStateResult(validationResult);
            }

            var creationResult = new ModifyDbStateResult { IsSuccessful = true };
            try
            {
                await action();
            }
            catch (Exception)
            {
                creationResult.IsSuccessful = false;
                creationResult.Errors.Add(errorMessage);
            }

            return creationResult;
        }

        protected async Task<ModifyDbStateResult> UpdateEntityAsync<TEntity>(TEntity entity, string errorMessage, Func<Task> action) where TEntity : Entity
        {
            var validationResult = entity.Validate();
            if (!validationResult.IsValid)
            {
                return new ModifyDbStateResult(validationResult);
            }

            var updatingResult = new ModifyDbStateResult { IsSuccessful = true };
            try
            {
                await action();
            }
            catch (Exception)
            {
                updatingResult.IsSuccessful = false;
                updatingResult.Errors.Add(errorMessage);
            }

            return updatingResult;
        }

        protected async Task<ModifyDbStateResult> RemoveEntityAsync(int id, string errorMessage, Func<Task> action)
        {
            var result = new ModifyDbStateResult { IsSuccessful = true };
            try
            {
                await action();
            }
            catch (Exception)
            {
                result.IsSuccessful = false;
                result.Errors.Add(errorMessage);
            }

            return result;
        }
    }
}
