using FluentMigrator;

namespace VueTest.Utilities.Migrations
{
    [Migration(201912080001)]
    public class CreateUser : Migration
    {
        public override void Down()
        {
            Delete.Table("Users");
        }

        public override void Up()
        {
            Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Firstname").AsString().NotNullable()
            .WithColumn("Lastname").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Gender").AsInt32().NotNullable()
            .WithColumn("Age").AsInt32().NotNullable();
        }
    }
}