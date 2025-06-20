using System.Data;

namespace NarakuShonin.DataAccessLayer;

public interface IUnitOfWork: IDisposable
{
  IDbConnection Connection { get; }
  IDbTransaction? Transaction { get; }
  void BeginTransaction();
  void CommitTransaction();
  void RollbackTransaction();
}