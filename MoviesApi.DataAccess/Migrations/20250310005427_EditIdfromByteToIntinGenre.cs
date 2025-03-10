using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditIdfromByteToIntinGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop the primary key
            migrationBuilder.DropPrimaryKey("PK_Genres", "Genres");

            // 2. Alter the column type from byte to int
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                nullable: false
            );

            // 3. Recreate the primary key
            migrationBuilder.AddPrimaryKey("PK_Genres", "Genres", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rollback changes (if needed)
            migrationBuilder.DropPrimaryKey("PK_Genres", "Genres");

            migrationBuilder.AlterColumn<byte>(
                name: "Id",
                table: "Genres",
                nullable: false
            );

            migrationBuilder.AddPrimaryKey("PK_Genres", "Genres", "Id");
        }

    }
}
