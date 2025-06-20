using System.Data;
using Npgsql;

namespace NarakuShonin.DataAccessLayer;

public class UnitOfWork: IUnitOfWork
{
  private readonly IConnectionStringProvider _connectionStringProvider;
  private IDbConnection _connection;
  private IDbTransaction? _transaction = null;
  
  IDbConnection IUnitOfWork.Connection => _connection;
  IDbTransaction? IUnitOfWork.Transaction => _transaction;

  public UnitOfWork(IConnectionStringProvider connectionStringProvider)
  {
    _connectionStringProvider = connectionStringProvider;
    _connection = new NpgsqlConnection(_connectionStringProvider.ConnectionString);
    _connection.Open();
  }
  
  public void Dispose()
  {
    if(_transaction != null)
      _transaction.Dispose();
    
    _connection.Dispose();
  }

  public void BeginTransaction()
  {
    _transaction = _connection.BeginTransaction();
  }

  public void CommitTransaction()
  {
    if (_transaction == null)
    {
      throw new Exception("Transaction has not been opened");
    }
    _transaction.Commit();
    Dispose();
  }

  public void RollbackTransaction()
  {
    if (_transaction == null)
    {
      throw new Exception("Transaction has not been opened");
    }
    _transaction.Rollback();
    Dispose();
  }
}