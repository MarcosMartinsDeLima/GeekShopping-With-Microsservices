using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekShopping.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class setcoupondata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "id", "CouponCode", "DiscountAmout" },
                values: new object[,]
                {
                    { 1L, "ERUDIO_2022_10", 10m },
                    { 2L, "ERUDIO_2022_15", 15m },
                    { 3L, "marcos", 25m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "id",
                keyValue: 3L);
        }
    }
}
