CREATE TYPE classlevel AS ENUM ('A', 'B', 'C', 'D', 'E', 'F');
CREATE TYPE visibility AS ENUM ('private', 'public');
CREATE TYPE role AS ENUM ('student', 'teacher', 'admin');
CREATE TYPE questiontype AS ENUM ('digital', 'analog');


CREATE TABLE Appendix
(
  id         integer NOT NULL,
  name       varchar NOT NULL,
  url        varchar NOT NULL,
  isDeleted  bool    NOT NULL,
  questionId integer NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON TABLE Appendix IS '(Billag)';

CREATE TABLE Classes
(
  id         integer     NOT NULL,
  name       varchar     NOT NULL,
  year       timestamptz NOT NULL,
  classLevel classlevel  NOT NULL,
  isDeleted  bool        NOT NULL,
  courseId   integer     NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON COLUMN Classes.classLevel IS 'A, B, C...';

CREATE TABLE ContentType
(
  id        integer NOT NULL,
  name      varchar,
  isDeleted bool    NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON TABLE ContentType IS '(diagram, tekst)';

CREATE TABLE Courses
(
  id   integer NOT NULL,
  name varchar NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON COLUMN Courses.name IS 'mat, fys, etc.';

CREATE TABLE ErrorTypes
(
  errorType integer NOT NULL,
  name      varchar NOT NULL,
  userId    integer NOT NULL,
  PRIMARY KEY (errorType)
);

CREATE TABLE QuestionAnswers
(
  id            integer    NOT NULL,
  name          varchar   ,
  url           varchar   ,
  visibility    visibility NOT NULL,
  isDeleted     bool       NOT NULL,
  questionId    integer    NOT NULL,
  questionSetId integer    NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON COLUMN QuestionAnswers.questionSetId IS 'f.feks. svar til hele questionset i stedet for kun 1 question';

CREATE TABLE QuestionAnswerTypes
(
  questionAnswerId integer NOT NULL,
  contentTypeId    integer NOT NULL
);

CREATE TABLE QuestionCollections
(
  id         integer NOT NULL,
  points     varchar,
  sequence   int    ,
  questionId integer NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Questions
(
  id         integer      NOT NULL,
  points     varchar      NOT NULL,
  type       questiontype NOT NULL,
  classLevel classlevel   NOT NULL,
  isDeleted  bool         NOT NULL,
  userId     integer      NOT NULL,
  courseId   integer      NOT NULL,
  questionId integer     ,
  PRIMARY KEY (id)
);

COMMENT ON COLUMN Questions.type IS 'delprøve';

COMMENT ON COLUMN Questions.classLevel IS 'A,B,C';

CREATE TABLE QuestionSets
(
  id        integer NOT NULL,
  name      varchar,
  isExam    bool    NOT NULL,
  isDraft   bool    NOT NULL,
  isDeleted bool    NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON COLUMN QuestionSets.isDraft IS 'if its assigned to students';

CREATE TABLE QuestionsQuestionSets
(
  questionId    integer NOT NULL,
  questionSetId integer NOT NULL
);

CREATE TABLE QuestionsSubjects
(
  questionId integer NOT NULL,
  subjectId  integer NOT NULL
);

COMMENT ON COLUMN QuestionsSubjects.subjectId IS 'trigonometri, vectors';

CREATE TABLE QuestionsTypes
(
  questionId    integer NOT NULL,
  contentTypeId integer NOT NULL
);

CREATE TABLE StudentResultErrorTypes
(
  studentResultId integer NOT NULL,
  errorType       integer NOT NULL
);

CREATE TABLE StudentResults
(
  id                          integer     NOT NULL,
  teacherPoint                varchar    ,
  teacherFeedback             varchar    ,
  studentSelfAssessmentPoints varchar    ,
  createdDate                 timestamptz NOT NULL,
  isDeleted                   bool        NOT NULL,
  userId                      integer     NOT NULL,
  questionId                  integer     NOT NULL,
  questionSetId               integer     NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Subjects
(
  id      integer NOT NULL,
  subject varchar NOT NULL,
  PRIMARY KEY (id)
);

COMMENT ON COLUMN Subjects.subject IS 'trigonometri, vectors';

CREATE TABLE Users
(
  id        integer NOT NULL UNIQUE,
  firstName varchar NOT NULL,
  lastName  varchar NOT NULL,
  email     varchar NOT NULL,
  role      role    NOT NULL,
  isDeleted bool    NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE UsersClasses
(
  classId integer NOT NULL,
  userId  integer NOT NULL
);

ALTER TABLE UsersClasses
  ADD CONSTRAINT FK_Classes_TO_UsersClasses
    FOREIGN KEY (classId)
    REFERENCES Classes (id);

ALTER TABLE UsersClasses
  ADD CONSTRAINT FK_Users_TO_UsersClasses
    FOREIGN KEY (userId)
    REFERENCES Users (id);

ALTER TABLE QuestionAnswers
  ADD CONSTRAINT FK_Questions_TO_QuestionAnswers
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE QuestionsTypes
  ADD CONSTRAINT FK_Questions_TO_QuestionsTypes
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE QuestionsTypes
  ADD CONSTRAINT FK_ContentType_TO_QuestionsTypes
    FOREIGN KEY (contentTypeId)
    REFERENCES ContentType (id);

ALTER TABLE QuestionAnswerTypes
  ADD CONSTRAINT FK_QuestionAnswers_TO_QuestionAnswerTypes
    FOREIGN KEY (questionAnswerId)
    REFERENCES QuestionAnswers (id);

ALTER TABLE QuestionAnswerTypes
  ADD CONSTRAINT FK_ContentType_TO_QuestionAnswerTypes
    FOREIGN KEY (contentTypeId)
    REFERENCES ContentType (id);

ALTER TABLE QuestionCollections
  ADD CONSTRAINT FK_Questions_TO_QuestionCollections
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE StudentResults
  ADD CONSTRAINT FK_Users_TO_StudentResults
    FOREIGN KEY (userId)
    REFERENCES Users (id);

ALTER TABLE QuestionsQuestionSets
  ADD CONSTRAINT FK_Questions_TO_QuestionsQuestionSets
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE QuestionsQuestionSets
  ADD CONSTRAINT FK_QuestionSets_TO_QuestionsQuestionSets
    FOREIGN KEY (questionSetId)
    REFERENCES QuestionSets (id);

ALTER TABLE StudentResults
  ADD CONSTRAINT FK_Questions_TO_StudentResults
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE StudentResults
  ADD CONSTRAINT FK_QuestionSets_TO_StudentResults
    FOREIGN KEY (questionSetId)
    REFERENCES QuestionSets (id);

ALTER TABLE StudentResultErrorTypes
  ADD CONSTRAINT FK_StudentResults_TO_StudentResultErrorTypes
    FOREIGN KEY (studentResultId)
    REFERENCES StudentResults (id);

ALTER TABLE StudentResultErrorTypes
  ADD CONSTRAINT FK_ErrorTypes_TO_StudentResultErrorTypes
    FOREIGN KEY (errorType)
    REFERENCES ErrorTypes (errorType);

ALTER TABLE Questions
  ADD CONSTRAINT FK_Users_TO_Questions
    FOREIGN KEY (userId)
    REFERENCES Users (id);

ALTER TABLE Questions
  ADD CONSTRAINT FK_Courses_TO_Questions
    FOREIGN KEY (courseId)
    REFERENCES Courses (id);

ALTER TABLE QuestionsSubjects
  ADD CONSTRAINT FK_Questions_TO_QuestionsSubjects
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE QuestionsSubjects
  ADD CONSTRAINT FK_Subjects_TO_QuestionsSubjects
    FOREIGN KEY (subjectId)
    REFERENCES Subjects (id);

ALTER TABLE QuestionAnswers
  ADD CONSTRAINT FK_QuestionSets_TO_QuestionAnswers
    FOREIGN KEY (questionSetId)
    REFERENCES QuestionSets (id);

ALTER TABLE ErrorTypes
  ADD CONSTRAINT FK_Users_TO_ErrorTypes
    FOREIGN KEY (userId)
    REFERENCES Users (id);

ALTER TABLE Appendix
  ADD CONSTRAINT FK_Questions_TO_Appendix
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE Questions
  ADD CONSTRAINT FK_Questions_TO_Questions
    FOREIGN KEY (questionId)
    REFERENCES Questions (id);

ALTER TABLE Classes
  ADD CONSTRAINT FK_Courses_TO_Classes
    FOREIGN KEY (courseId)
    REFERENCES Courses (id);
