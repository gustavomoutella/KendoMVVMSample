using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Vsol.Api.Shared.Infra.Data.Transactions
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbConnection _connection;
        private DbTransaction _transaction;
        private bool _disposed;

        private ICollection<DbContext> _contexts;

        public UnitOfWork()
        {
            _connection = new SqlConnection(AppSettings.ConnectionStrings.DefaultConnection);
            _contexts = new List<DbContext>();
        }

        public DbConnection Connection
        {
            get { return _connection; }
        }

        public DbTransaction Transaction
        {
            get { return _transaction; }
        }

        public void AddContext(DbContext context)
        {
            if (!_contexts.Contains(context))
                _contexts.Add(context);
        }

        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                if (string.IsNullOrEmpty(_connection.ConnectionString))
                {
                    _connection.ConnectionString = AppSettings.ConnectionStrings.DefaultConnection;
                    _contexts = new List<DbContext>();
                }

                _connection.Open();
            }

            _transaction = _connection.BeginTransaction();

            foreach (var context in _contexts)
            {
                context.Database.UseTransaction(_transaction);
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _connection.Close();
                _connection.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction.Connection != null)
                    _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _connection.Close();
                _connection.Dispose();
            }
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
                if (disposing)
                {
                    if (_transaction != null)
                        _transaction.Dispose();

                    if (_connection != null)
                        _connection.Dispose();

                    _contexts.Clear();
                }

                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}