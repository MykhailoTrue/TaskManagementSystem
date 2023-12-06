using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TMS.EF.NTier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IssueTypes",
                columns: new[] { "Id", "Description", "Name", "ProjectId" },
                values: new object[,]
                {
                    { 1, "Neque dolorem corporis accusantium sequi quibusdam id qui. Aut ut sed omnis et odit. Quisquam qui culpa quis voluptatem et et iusto consequatur adipisci. Eum doloribus quas. Et qui vitae praesentium sint suscipit provident et.", "inventore", 2 },
                    { 2, "A non rerum magnam rerum. Odio nemo et deleniti. Consequatur exercitationem aut accusamus eius.", "sint", 12 },
                    { 3, "Debitis ipsam dolore facere optio laudantium. Et amet laboriosam ex. Illo cupiditate odit et molestiae. Dolores deserunt omnis velit nesciunt quia consectetur quo neque illum. Ipsam quis dignissimos qui aut laudantium. Minima dolores iste quam quae repellendus sed neque.", "omnis", 1 },
                    { 4, "Asperiores sapiente nostrum perferendis facilis sit pariatur soluta sequi. Natus voluptatibus ullam. Sit officiis illo voluptates tempora dolore. Laudantium et autem.", "saepe", 3 },
                    { 5, "Beatae repellat quae exercitationem in reprehenderit qui molestias voluptatem. Quibusdam sapiente tenetur dolor corrupti. Sunt in quia libero. Dolore aut numquam libero et impedit non. Dolores fuga recusandae quidem vitae.", "quo", 5 },
                    { 6, "Culpa voluptates omnis cum pariatur. Aut voluptatem nostrum minima reiciendis nihil modi vel officia. Neque fugiat necessitatibus fugiat et suscipit soluta exercitationem quia cupiditate.", "nesciunt", 5 },
                    { 7, "Dolor voluptas quia nemo asperiores non sed molestiae in. Odit ea est et omnis. Ad consequatur nisi possimus et assumenda enim velit quidem et. Quaerat quas quia magni.", "et", 2 },
                    { 8, "Est dolore sint. Autem omnis ut iusto natus qui tempora error magnam. Voluptatem magnam voluptas occaecati similique hic velit inventore. Libero placeat maiores vero nihil architecto velit vero laudantium aliquid.", "itaque", 3 },
                    { 9, "Doloribus delectus repellat nobis suscipit. Rem velit soluta reiciendis sint labore architecto nostrum. Quis rem est officia ut dolore quasi reprehenderit. Vel ea commodi accusamus omnis non sint voluptas mollitia saepe. Omnis sunt voluptatem architecto ut molestias quaerat et consequatur.", "ut", 3 },
                    { 10, "Magni explicabo aut rerum enim qui aut sint sunt explicabo. Aut ut animi est rem inventore cum rerum sit. Quia quibusdam quia et labore omnis.", "sit", 12 }
                });

            migrationBuilder.InsertData(
                table: "ProjectColumns",
                columns: new[] { "Id", "Name", "ProjectId" },
                values: new object[,]
                {
                    { 1, "repellat", 5 },
                    { 2, "fuga", 1 },
                    { 3, "rerum", 4 },
                    { 4, "in", 1 },
                    { 5, "occaecati", 5 },
                    { 6, "est", 12 },
                    { 7, "et", 1 },
                    { 8, "accusantium", 8 },
                    { 9, "nisi", 3 },
                    { 10, "accusantium", 1 }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AsigneeId", "Descritption", "IssueTypeId", "Name", "ProjectColumnId" },
                values: new object[,]
                {
                    { 1, 1007, "Error et ex blanditiis quod libero.", 3, "alias", 4 },
                    { 2, 1007, "Recusandae reiciendis voluptatum reprehenderit aspernatur alias voluptates laborum mollitia.", 7, "sed", 10 },
                    { 3, 1, "A exercitationem tempore voluptatem architecto quidem.", 10, "qui", 2 },
                    { 4, 1, "Dolorum saepe eos voluptatibus.", 8, "non", 2 },
                    { 5, 2, "Molestias adipisci quia vel sint labore.", 4, "odio", 9 },
                    { 6, 1015, "Dicta sed maiores repellendus quis expedita aliquam blanditiis similique distinctio.", 8, "dignissimos", 1 },
                    { 7, 1015, "Odit quae et blanditiis dolorem excepturi.", 5, "ratione", 2 },
                    { 8, 1007, "Vero molestias fugiat dolor quod excepturi maxime et nesciunt vitae.", 1, "omnis", 10 },
                    { 9, 1015, "Optio officiis quod quos ea autem libero.", 2, "atque", 1 },
                    { 10, 1007, "Ad reprehenderit maiores fugit voluptatem voluptatem qui quibusdam placeat dolorum.", 10, "placeat", 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProjectColumns",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
