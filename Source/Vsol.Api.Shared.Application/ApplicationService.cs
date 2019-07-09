using System;
using System.Linq;
using Vsol.Api.Shared.Domain;
using Vsol.Api.Shared.Infra.Data.Transactions;

namespace Vsol.Api.Shared.Application
{
    public abstract class ApplicationService
    {
        protected readonly IUnitOfWork _uow;
        private NotificationResult result;

        public ApplicationService(IUnitOfWork uow)
        {
            this._uow = uow;
            result = new NotificationResult();
        }

        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public NotificationResult Commit(NotificationResult result)
        {
            if (result.IsValid)
            {
                try
                {
                    _uow.Commit();
                }
                catch (Exception ex)
                {
                    _uow.Rollback();
                    result.AddError(ex);
                }
            }
            else
            {
                _uow.Rollback();
            }

            return result;
        }
    }
}
