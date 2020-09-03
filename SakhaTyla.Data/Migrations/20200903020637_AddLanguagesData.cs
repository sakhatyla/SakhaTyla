using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhaTyla.Data.Migrations
{
    public partial class AddLanguagesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
set identity_insert Languages on
insert into Languages (Id, Name, Code, CreationDate, ModificationDate) values
(1, N'Английский', N'en', getdate(), getdate()),
(2, N'Русский', N'ru', getdate(), getdate()),
(3, N'Якутский', N'sah', getdate(), getdate())
set identity_insert Languages off");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
