using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace feedbackFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:class_level", "a,b,c,d,e,f")
                .Annotation("Npgsql:Enum:question_type", "digital,analog")
                .Annotation("Npgsql:Enum:user_role", "student,teacher,admin")
                .Annotation("Npgsql:Enum:visibility", "private,public");

            migrationBuilder.CreateTable(
                name: "content_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
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
                    name = table.Column<string>(type: "character varying", nullable: false, comment: "mat, fys, etc.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("courses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "questionsets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    is_exam = table.Column<bool>(type: "boolean", nullable: false),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false, comment: "if its assigned to students"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("questionsets_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    subject = table.Column<string>(type: "character varying", nullable: false, comment: "trigonometri, vectors")
                },
                constraints: table =>
                {
                    table.PrimaryKey("subjects_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    firstname = table.Column<string>(type: "character varying", nullable: false),
                    lastname = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    user_role = table.Column<int>(type: "user_role", nullable: false)
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
                    year = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    class_level = table.Column<int>(type: "class_level", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    course_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_courses_to_classes",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "error_types",
                columns: table => new
                {
                    error_type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("errortypes_pkey", x => x.error_type);
                    table.ForeignKey(
                        name: "fk_users_to_errortypes",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    points = table.Column<string>(type: "character varying", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    question_type = table.Column<int>(type: "question_type", nullable: false),
                    class_level = table.Column<int>(type: "class_level", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    course_id = table.Column<int>(type: "integer", nullable: true),
                    question_id = table.Column<int>(type: "integer", nullable: true)
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
                        name: "fk_questions_to_questions",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_to_questions",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ClassUser",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUser", x => new { x.ClassesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ClassUser_classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassUser_users_UsersId",
                        column: x => x.UsersId,
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
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
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
                        name: "FK_ContentTypeQuestion_content_type_ContentTypesId",
                        column: x => x.ContentTypesId,
                        principalTable: "content_type",
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
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_set_id = table.Column<int>(type: "integer", nullable: false, comment: "f.feks. svar til hele questionset i stedet for kun 1 question")
                },
                constraints: table =>
                {
                    table.PrimaryKey("questionanswers_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_questionanswers",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_questionsets_to_questionanswers",
                        column: x => x.question_set_id,
                        principalTable: "questionsets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "questioncollections",
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
                    table.PrimaryKey("questioncollections_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_questioncollections",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionQuestionSet",
                columns: table => new
                {
                    QuestionSetsId = table.Column<int>(type: "integer", nullable: false),
                    QuestionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionQuestionSet", x => new { x.QuestionSetsId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_QuestionQuestionSet_questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionQuestionSet_questionsets_QuestionSetsId",
                        column: x => x.QuestionSetsId,
                        principalTable: "questionsets",
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
                name: "StudentResults",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    teacher_point = table.Column<string>(type: "character varying", nullable: true),
                    teacher_feedback = table.Column<string>(type: "character varying", nullable: true),
                    student_self_assessment_points = table.Column<string>(type: "character varying", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    question_set_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentresults_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_to_studentresults",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_questionsets_to_studentresults",
                        column: x => x.question_set_id,
                        principalTable: "questionsets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_to_studentresults",
                        column: x => x.user_id,
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
                        name: "FK_ContentTypeQuestionAnswer_content_type_ContentTypesId",
                        column: x => x.ContentTypesId,
                        principalTable: "content_type",
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
                name: "ErrorTypeStudentResult",
                columns: table => new
                {
                    ErrorTypesId = table.Column<int>(type: "integer", nullable: false),
                    StudentResultsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorTypeStudentResult", x => new { x.ErrorTypesId, x.StudentResultsId });
                    table.ForeignKey(
                        name: "FK_ErrorTypeStudentResult_StudentResults_StudentResultsId",
                        column: x => x.StudentResultsId,
                        principalTable: "StudentResults",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErrorTypeStudentResult_error_types_ErrorTypesId",
                        column: x => x.ErrorTypesId,
                        principalTable: "error_types",
                        principalColumn: "error_type",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ClassUser_UsersId",
                table: "ClassUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeQuestion_QuestionsId",
                table: "ContentTypeQuestion",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeQuestionAnswer_QuestionAnswersId",
                table: "ContentTypeQuestionAnswer",
                column: "QuestionAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_error_types_user_id",
                table: "error_types",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorTypeStudentResult_StudentResultsId",
                table: "ErrorTypeStudentResult",
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
                name: "IX_questioncollections_question_id",
                table: "questioncollections",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuestionSet_QuestionsId",
                table: "QuestionQuestionSet",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_course_id",
                table: "questions",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_question_id",
                table: "questions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_user_id",
                table: "questions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSubject_SubjectsId",
                table: "QuestionSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentResults_question_id",
                table: "StudentResults",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentResults_question_set_id",
                table: "StudentResults",
                column: "question_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentResults_user_id",
                table: "StudentResults",
                column: "user_id");
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
                name: "ErrorTypeStudentResult");

            migrationBuilder.DropTable(
                name: "questioncollections");

            migrationBuilder.DropTable(
                name: "QuestionQuestionSet");

            migrationBuilder.DropTable(
                name: "QuestionSubject");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "content_type");

            migrationBuilder.DropTable(
                name: "question_answers");

            migrationBuilder.DropTable(
                name: "StudentResults");

            migrationBuilder.DropTable(
                name: "error_types");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "questionsets");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
