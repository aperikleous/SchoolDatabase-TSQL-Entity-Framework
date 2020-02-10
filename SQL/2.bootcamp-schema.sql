

-- ************************************** [Courses]
USE PerikleousBootcamp;
CREATE TABLE [Courses]
(
 [courseId]   int NOT NULL IDENTITY(1,1),
 [title]      nvarchar(50) NOT NULL ,
 [stream]     nvarchar(50) NOT NULL ,
 [type]       nvarchar(50) NOT NULL ,
 [start_date] date NOT NULL ,
 [end_date]   date NOT NULL ,


 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([courseId] ASC)
);
GO



-- ************************************** [Assignments]
USE PerikleousBootcamp;
CREATE TABLE [Assignments]
(
 [assignmentId] int NOT NULL IDENTITY(1,1),
 [title]        nvarchar(50) NOT NULL ,
 [description]  nvarchar(50) NOT NULL ,
 [subDateTime]  datetime NOT NULL ,
 [oralMark]     int NOT NULL ,
 [totalMark]    int NOT NULL ,
 [courseId]     int NOT NULL ,


 CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED ([assignmentId] ASC),
 CONSTRAINT [FK_68] FOREIGN KEY ([courseId])  REFERENCES [Courses]([courseId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_68] ON [Assignments]
 (
  [courseId] ASC
 )

GO

-- ************************************** [Trainers]
USE PerikleousBootcamp;
CREATE TABLE [Trainers]
(
 [trainerId] int NOT NULL IDENTITY(1,1),
 [firstName] nvarchar(50) NOT NULL ,
 [lastName]  nvarchar(50) NOT NULL ,
 [subject]   nvarchar(50) NOT NULL ,


 CONSTRAINT [PK_Trainers] PRIMARY KEY CLUSTERED ([trainerId] ASC)
);
GO


-- ************************************** [Students]
USE PerikleousBootcamp;
CREATE TABLE [Students]
(
 [studentId]   int NOT NULL IDENTITY(1,1),
 [firstName]   nvarchar(50) NOT NULL ,
 [lastName]    nvarchar(50) NOT NULL ,
 [dateOfBirth] date NOT NULL ,
 [tuitionFees] int NOT NULL ,


 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([studentId] ASC)
);
GO

-- ************************************** [CourseTrainers]
USE PerikleousBootcamp;
CREATE TABLE [CourseTrainers]
(
 [courseId]  int NOT NULL ,
 [trainerId] int NOT NULL ,


 CONSTRAINT [PK_CourseTrainers] PRIMARY KEY CLUSTERED ([courseId] ASC, [trainerId] ASC),
 CONSTRAINT [FK_82] FOREIGN KEY ([courseId])  REFERENCES [Courses]([courseId]),
 CONSTRAINT [FK_86] FOREIGN KEY ([trainerId])  REFERENCES [Trainers]([trainerId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_82] ON [CourseTrainers]
 (
  [courseId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_86] ON [CourseTrainers]
 (
  [trainerId] ASC
 )

GO


-- ************************************** [CourseStudents]
USE PerikleousBootcamp;
CREATE TABLE [CourseStudents]
(
 [courseId]  int NOT NULL ,
 [studentId] int NOT NULL ,


 CONSTRAINT [PK_CourseStudents] PRIMARY KEY CLUSTERED ([courseId] ASC, [studentId] ASC),
 CONSTRAINT [FK_58] FOREIGN KEY ([courseId])  REFERENCES [Courses]([courseId]),
 CONSTRAINT [FK_64] FOREIGN KEY ([studentId])  REFERENCES [Students]([studentId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_58] ON [CourseStudents]
 (
  [courseId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_64] ON [CourseStudents]
 (
  [studentId] ASC
 )

GO
