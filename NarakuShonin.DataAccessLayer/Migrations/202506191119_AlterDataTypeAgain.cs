using FluentMigrator;

namespace NarakuShonin.DataAccessLayer.Migrations;

[Migration(202506191119)]
public class AlterDataTypeAgain : Migration 
{
  public override void Up()
  {
    Alter.Table("GuildConfigurations")
      .AlterColumn("Id")
      .AsString();
  }

  public override void Down()
  {
    Alter.Table("GuildConfigurations")
      .AlterColumn("Id")
      .AsCustom("NUMERIC");
  }
}