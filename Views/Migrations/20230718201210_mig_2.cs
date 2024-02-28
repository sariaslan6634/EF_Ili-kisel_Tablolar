using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Views.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Bu migration ne zaman oluşturulsa o zaman view olur
            //View olusturmak icin gerekli sorguyu yazıyoruz
            migrationBuilder.Sql($@"
            CREATE VIEW vm_PersonOrders
            AS
                SELECT TOP 100 p.Name , COUNT(*) [Count] FROM Persons p 
                INNER JOIN Orders o
                    ON p.PersonId = o.PersonId
                GROUP By p.Name
                ORDER By [Count] DESC
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Bu migration ne zaman silinirse o zaman bu view silinir.
            //Oluşturulan View'i silmek istiyorsak
            migrationBuilder.Sql($@"DROP VIEW vm_PersonOrders");
        }
    }
}
