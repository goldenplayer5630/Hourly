using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hourly.Data.Migrations
{
    /// <inheritdoc />
    public partial class initCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "git_repository",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ext_repository_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    @namespace = table.Column<string>(name: "namespace", type: "character varying(255)", maxLength: 255, nullable: false),
                    web_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_repository", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    permissions = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    department_id = table.Column<Guid>(type: "uuid", nullable: true),
                    git_email = table.Column<string>(type: "text", nullable: true),
                    git_username = table.Column<string>(type: "text", nullable: true),
                    git_access_token = table.Column<string>(type: "text", nullable: true),
                    tvt_hour_balance = table.Column<float>(type: "real", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_department",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_role",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "git_commits",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ext_commit_id = table.Column<string>(type: "text", nullable: false),
                    ext_commit_short_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    authored_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    web_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_commits", x => x.id);
                    table.ForeignKey(
                        name: "FK_git_commits_git_repository_repository_id",
                        column: x => x.repository_id,
                        principalTable: "git_repository",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_git_commit_author",
                        column: x => x.author_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_contract",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    contract_type = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    min_weekly_hours = table.Column<double>(type: "double precision", nullable: false),
                    max_weekly_hours = table.Column<double>(type: "double precision", nullable: false),
                    gross_hourly_rate = table.Column<double>(type: "double precision", nullable: true),
                    holiday_hours_percentage = table.Column<int>(type: "integer", nullable: true),
                    monthly_paid_holiday_hours = table.Column<bool>(type: "boolean", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    contract_file_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_contract", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_contract_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_session",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    break_time = table.Column<float>(type: "real", nullable: false),
                    factor = table.Column<float>(type: "real", nullable: false),
                    tvt_accrued_hours = table.Column<float>(type: "real", nullable: false),
                    tvt_used_hours = table.Column<float>(type: "real", nullable: false),
                    wbso = table.Column<bool>(type: "boolean", nullable: false),
                    locked = table.Column<bool>(type: "boolean", nullable: false),
                    other_remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_session", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_session_user_contract_id",
                        column: x => x.user_contract_id,
                        principalTable: "user_contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "git_commit_work_sessions",
                columns: table => new
                {
                    git_commit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_session_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_commit_work_sessions", x => new { x.git_commit_id, x.work_session_id });
                    table.ForeignKey(
                        name: "FK_git_commit_work_sessions_git_commits_git_commit_id",
                        column: x => x.git_commit_id,
                        principalTable: "git_commits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_git_commit_work_sessions_work_session_work_session_id",
                        column: x => x.work_session_id,
                        principalTable: "work_session",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_git_commit_work_sessions_work_session_id",
                table: "git_commit_work_sessions",
                column: "work_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_git_commits_author_id",
                table: "git_commits",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_git_commits_repository_id",
                table: "git_commits",
                column: "repository_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_department_id",
                table: "user",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_contract_user_id",
                table: "user_contract",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_session_user_contract_id",
                table: "work_session",
                column: "user_contract_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "git_commit_work_sessions");

            migrationBuilder.DropTable(
                name: "git_commits");

            migrationBuilder.DropTable(
                name: "work_session");

            migrationBuilder.DropTable(
                name: "git_repository");

            migrationBuilder.DropTable(
                name: "user_contract");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
