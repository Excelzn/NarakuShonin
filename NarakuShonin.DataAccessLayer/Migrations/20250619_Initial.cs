using FluentMigrator;

namespace NarakuShonin.DataAccessLayer.Migrations;

[Migration(202506191015)]
public class Initial: Migration
{
  public override void Up()
  {
    Create.Table("GuildConfigurations")
      .WithColumn("Id").AsInt64().PrimaryKey();
  }

  public override void Down()
  {
    Delete.Table("GuildConfigurations");
  }
}