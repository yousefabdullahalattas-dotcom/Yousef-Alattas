using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootCamp_MVC_EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupplierAddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialType",
                table: "Suppliers",
                newName: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product",
                table: "Suppliers",
                newName: "MaterialType");
        }
    }
}
