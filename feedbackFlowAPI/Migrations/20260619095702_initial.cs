using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace feedbackFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:class_level", "a,b,c,d")
                .Annotation("Npgsql:Enum:education", "hf,hhx,htx,stx")
                .Annotation("Npgsql:Enum:exam_type", "digital,analog")
                .Annotation("Npgsql:Enum:new_old_system", "new,old")
                .Annotation("Npgsql:Enum:question_context", "yes_heavy,yes_light,no")
                .Annotation("Npgsql:Enum:question_difficulty", "hard,medium,easy")
                .Annotation("Npgsql:Enum:question_method_requirement", "apply_formula,specific_method,no_requirement")
                .Annotation("Npgsql:Enum:standard_question", "yes,partially,no,with_a_twist")
                .Annotation("Npgsql:Enum:visibility", "private,public");

            migrationBuilder.CreateTable(
                name: "content_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("contenttype_pkey", x => x.id);
                },
                comment: "(diagram, tekst)");

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courses_pkey", x => x.id);
                },
                comment: "mat, fys, etc.");

            migrationBuilder.CreateTable(
                name: "mistakes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("mistakes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    subject = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subjects_pkey", x => x.id);
                },
                comment: "trigonometri, vectors");

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userroles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    first_name = table.Column<string>(type: "character varying", nullable: false),
                    last_name = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    year = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    education = table.Column<int>(type: "education", nullable: false),
                    class_level = table.Column<int>(type: "class_level", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_courses_to_classes",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teacher_to_classes",
                        column: x => x.TeacherId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question_sets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    is_exam = table.Column<bool>(type: "boolean", nullable: false),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false, comment: "if its assigned to students"),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    teacher_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("question_sets_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_teacher_to_questionset",
                        column: x => x.teacher_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    img_src = table.Column<string>(type: "character varying", nullable: false),
                    points = table.Column<string>(type: "character varying", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    exam_type = table.Column<int>(type: "exam_type", nullable: false),
                    class_level = table.Column<int>(type: "class_level", nullable: false),
                    question_difficulty = table.Column<int>(type: "question_difficulty", nullable: false),
                    question_method_requirement = table.Column<int>(type: "question_method_requirement", nullable: false),
                    education = table.Column<int>(type: "education", nullable: false),
                    question_context = table.Column<int>(type: "question_context", nullable: false),
                    standard_question = table.Column<int>(type: "standard_question", nullable: false),
                    new_old_system = table.Column<int>(type: "new_old_system", nullable: false),
                    course_id = table.Column<int>(type: "integer", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("questions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_courses_to_questions",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_to_questions",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserUserRole",
                columns: table => new
                {
                    UserRolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserRole", x => new { x.UserRolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUserRole_user_roles_UserRolesId",
                        column: x => x.UserRolesId,
                        principalTable: "user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUserRole_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassUser",
                columns: table => new
                {
                    StudentClassesId = table.Column<int>(type: "integer", nullable: false),
                    StudentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUser", x => new { x.StudentClassesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ClassUser_classes_StudentClassesId",
                        column: x => x.StudentClassesId,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassUser_users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appendix",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    url = table.Column<string>(type: "character varying", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    question_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("appendix_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_appendix",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                },
                comment: "(Billag)");

            migrationBuilder.CreateTable(
                name: "ContentTypeQuestion",
                columns: table => new
                {
                    ContentTypesId = table.Column<int>(type: "integer", nullable: false),
                    QuestionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypeQuestion", x => new { x.ContentTypesId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_ContentTypeQuestion_content_types_ContentTypesId",
                        column: x => x.ContentTypesId,
                        principalTable: "content_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentTypeQuestion_questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question_answers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    url = table.Column<string>(type: "character varying", nullable: true),
                    visibility = table.Column<int>(type: "visibility", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_set_id = table.Column<int>(type: "integer", nullable: false, comment: "f.feks. svar til hele questionset i stedet for kun 1 question")
                },
                constraints: table =>
                {
                    table.PrimaryKey("question_answers_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_questionanswers",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_questionsets_to_questionanswers",
                        column: x => x.question_set_id,
                        principalTable: "question_sets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "question_collections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    points = table.Column<string>(type: "character varying", nullable: true),
                    sequence = table.Column<int>(type: "integer", nullable: true),
                    question_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("question_collections_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_questioncollections",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "questions_question_sets",
                columns: table => new
                {
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_set_id = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions_question_sets", x => new { x.question_id, x.question_set_id, x.subject_id });
                    table.ForeignKey(
                        name: "fk_questions_to_questionquestionset",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_questionsets_to_questionquestionset",
                        column: x => x.question_set_id,
                        principalTable: "question_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subject_to_questionquestionset",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSubject",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSubject", x => new { x.QuestionsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_QuestionSubject_questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSubject_subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    teacher_point = table.Column<string>(type: "character varying", nullable: true),
                    teacher_feedback = table.Column<string>(type: "character varying", nullable: true),
                    student_self_assessment_points = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    teacher_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_set_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_results_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_studentresults",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_questionsets_to_studentresults",
                        column: x => x.question_set_id,
                        principalTable: "question_sets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_student_to_studentresults",
                        column: x => x.student_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_teacher_to_studentresults",
                        column: x => x.teacher_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ContentTypeQuestionAnswer",
                columns: table => new
                {
                    ContentTypesId = table.Column<int>(type: "integer", nullable: false),
                    QuestionAnswersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypeQuestionAnswer", x => new { x.ContentTypesId, x.QuestionAnswersId });
                    table.ForeignKey(
                        name: "FK_ContentTypeQuestionAnswer_content_types_ContentTypesId",
                        column: x => x.ContentTypesId,
                        principalTable: "content_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentTypeQuestionAnswer_question_answers_QuestionAnswersId",
                        column: x => x.QuestionAnswersId,
                        principalTable: "question_answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MistakeStudentResult",
                columns: table => new
                {
                    MistakesId = table.Column<int>(type: "integer", nullable: false),
                    StudentResultsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MistakeStudentResult", x => new { x.MistakesId, x.StudentResultsId });
                    table.ForeignKey(
                        name: "FK_MistakeStudentResult_mistakes_MistakesId",
                        column: x => x.MistakesId,
                        principalTable: "mistakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MistakeStudentResult_student_results_StudentResultsId",
                        column: x => x.StudentResultsId,
                        principalTable: "student_results",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "content_types",
                columns: new[] { "id", "deleted_at", "name" },
                values: new object[,]
                {
                    { 1, null, "Text" },
                    { 2, null, "Algebraic" },
                    { 3, null, "Graph" },
                    { 4, null, "Figure" },
                    { 5, null, "Table" }
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "Math" });

            migrationBuilder.InsertData(
                table: "mistakes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Presentation" },
                    { 2, "Documentation" },
                    { 3, "Argumentation" },
                    { 4, "Conclusion" },
                    { 5, "Calculation" },
                    { 6, "Insertion" },
                    { 7, "Inaccurate" },
                    { 8, "Technical" },
                    { 9, "Misunderstanding" }
                });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "subject" },
                values: new object[,]
                {
                    { 1, "Combinatorics" },
                    { 2, "Differential Calculus" },
                    { 3, "Quadratic polynomial" },
                    { 4, "Regression" },
                    { 5, "Exponential function" },
                    { 6, "Binomial distribution" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Teacher" },
                    { 3, "Student" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appendix_question_id",
                table: "appendix",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_classes_course_id",
                table: "classes",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_classes_TeacherId",
                table: "classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassUser_StudentsId",
                table: "ClassUser",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeQuestion_QuestionsId",
                table: "ContentTypeQuestion",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeQuestionAnswer_QuestionAnswersId",
                table: "ContentTypeQuestionAnswer",
                column: "QuestionAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_MistakeStudentResult_StudentResultsId",
                table: "MistakeStudentResult",
                column: "StudentResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_question_answers_question_id",
                table: "question_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_answers_question_set_id",
                table: "question_answers",
                column: "question_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_collections_question_id",
                table: "question_collections",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_sets_teacher_id",
                table: "question_sets",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_course_id",
                table: "questions",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_user_id",
                table: "questions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_question_sets_question_set_id",
                table: "questions_question_sets",
                column: "question_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_question_sets_subject_id",
                table: "questions_question_sets",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSubject_SubjectsId",
                table: "QuestionSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_student_results_question_id",
                table: "student_results",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_results_question_set_id",
                table: "student_results",
                column: "question_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_results_student_id",
                table: "student_results",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_results_teacher_id",
                table: "student_results",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserUserRole_UsersId",
                table: "UserUserRole",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appendix");

            migrationBuilder.DropTable(
                name: "ClassUser");

            migrationBuilder.DropTable(
                name: "ContentTypeQuestion");

            migrationBuilder.DropTable(
                name: "ContentTypeQuestionAnswer");

            migrationBuilder.DropTable(
                name: "MistakeStudentResult");

            migrationBuilder.DropTable(
                name: "question_collections");

            migrationBuilder.DropTable(
                name: "questions_question_sets");

            migrationBuilder.DropTable(
                name: "QuestionSubject");

            migrationBuilder.DropTable(
                name: "UserUserRole");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "content_types");

            migrationBuilder.DropTable(
                name: "question_answers");

            migrationBuilder.DropTable(
                name: "mistakes");

            migrationBuilder.DropTable(
                name: "student_results");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "question_sets");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
