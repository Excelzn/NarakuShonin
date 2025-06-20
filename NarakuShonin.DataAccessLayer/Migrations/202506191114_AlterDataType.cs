using FluentMigrator;

namespace NarakuShonin.DataAccessLayer.Migrations;

[Migration(202506191114)]
public class AlterDataType: Migration
{
  public override void Up()
  {
    Alter.Table("GuildConfigurations")
      .AlterColumn("Id")
      .AsCustom("NUMERIC");
  }

  public override void Down()
  {
    Alter.Table("GuildConfigurations")
      .AlterColumn("Id")
      .AsInt64();
  }
}