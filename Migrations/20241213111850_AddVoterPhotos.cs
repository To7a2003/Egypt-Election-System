using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectionSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddVoterPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "NationalIdPhoto",
                table: "Voters",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SelfiePhoto",
                table: "Voters",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalIdPhoto",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "SelfiePhoto",
                table: "Voters");
        }
    }
}
