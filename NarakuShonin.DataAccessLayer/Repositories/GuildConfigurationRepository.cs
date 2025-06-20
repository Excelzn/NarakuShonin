using Dapper;
using NarakuShonin.Api.Models;
using NarakuShonin.DataAccessLayer.Entities;
using NarakuShonin.Shared.Repositories;

namespace NarakuShonin.DataAccessLayer.Repositories;



public class GuildConfigurationRepository : IGuildConfigurationRepository
{
  private readonly IUnitOfWork _unitOfWork;

  public GuildConfigurationRepository(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<GuildConfigurationDto?> GetGuildConfiguration(string guildId)
  {
    var sql = """SELECT * FROM "GuildConfigurations" WHERE "Id" = @guildId""";
    return await _unitOfWork.Connection.
      QueryFirstOrDefaultAsync<GuildConfigurationDto>(sql, new { guildId });
    
  }

  public async Task CreateGuildConfiguration(GuildConfigurationDto guildConfiguration)
  {
    var sql = """INSERT INTO "GuildConfigurations" ("Id") VALUES (@Id) """;
    _unitOfWork.BeginTransaction();
    var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(sql, guildConfiguration);
    _unitOfWork.CommitTransaction();
  }

  public async Task<List<GuildConfigurationDto>> GetUserGuildConfigurations(List<string> guildIds)
  {
    var sql = """SELECT * FROM "GuildConfigurations" WHERE "Id" = ANY(@guildIds);""";
    return  (await _unitOfWork.Connection
      .QueryAsync<GuildConfigurationDto>(sql, new {guildIds})).ToList();
  }
}