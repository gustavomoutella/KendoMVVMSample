using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Vsol.Api.Shared.Infra.Data.Transactions
{
    public interface IUnitOfWork
    {
        DbConnection Connection { get; }

        DbTransaction Transaction { get; }

        void AddContext(DbContext context);

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}