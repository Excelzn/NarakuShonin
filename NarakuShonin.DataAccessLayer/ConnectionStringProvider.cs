namespace NarakuShonin.DataAccessLayer;

public class ConnectionStringProvider(string connectionString) : IConnectionStringProvider
{
  public string ConnectionString { get; } = connectionString;
}