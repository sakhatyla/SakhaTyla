using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhaTyla.Data.Migrations
{
    public partial class AddCategoriesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
set identity_insert Categories on
insert into Categories (Id, Name, CreationDate, ModificationDate) values
(1, N'Машиностроение', getdate(), getdate()),
(2, N'Мелиорация', getdate(), getdate()),
(3, N'Обогащение полезных ископаемых', getdate(), getdate()),
(4, N'Сварка', getdate(), getdate()),
(5, N'Сельское хозяйство', getdate(), getdate()),
(6, N'Транспорт', getdate(), getdate()),
(7, N'Химия', getdate(), getdate()),
(8, N'Энергетика и связь', getdate(), getdate())
set identity_insert Categories off");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
