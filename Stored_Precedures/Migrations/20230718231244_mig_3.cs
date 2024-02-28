using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stored_Precedures.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                CREATE PROCEDURE sp_bestSellingStaff
                AS
                    declare @name NVARCHAR(MAX), @count INT
                    SELECT TOP 1 @name = p.Name,@count = COUNT(*) FROM Persons p
                    JOIN Orders o
                        ON p.PersonId = o.PersonId
                    GROUP By p.Name
                    ORDER By COUNT(*) DESC
                    RETURN @count
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROC sp_bestSellingStaff");
        }
    }
}
