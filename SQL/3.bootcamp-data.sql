USE PerikleousBootcamp;

INSERT INTO Trainers
VALUES
	(N'Panagiotis',N'Bozas', N'OOP'),
	(N'Kostas',N'Stroggylos', N'SQL'),
  (N'George',N'Pasparakis', N'Databases'),
  (N'Ektoras',N'Gantzos', N'C#');

USE PerikleousBootcamp;
INSERT INTO Courses
VALUES
	(N'CB9',N'C#', N'Full-Time', '2019-11-11', '2020-03-01'),
  (N'CB8',N'C#', N'Part-Time', '2019-11-18', '2020-06-01'),
	(N'JB9',N'Java', N'Full-Time', '2019-11-11', '2020-06-03'),
  (N'JB8',N'Java', N'Part-Time', '2019-11-18', '2020-06-01');

USE PerikleousBootcamp;
INSERT INTO Assignments
VALUES
	(N'Chessmaster',N'Assign random pieces to chessboard', '2020-03-06', 30, 100, 1),
	(N'Databases',N'Connect to database', '2020-01-02', 20, 100, 1),
  (N'String assignment',N'Randomize string inputs', '2020-04-01', 15, 100, 2),
  (N'OOP Basics',N'Fundamentals of OOP', '2020-05-20', 30, 100, 3),
  (N'Headfirst',N'Advanced abstract classes', '2020-04-22', 30, 100, 4);

USE PerikleousBootcamp;
INSERT INTO Students
VALUES
  (N'Alex',N'Perikleous', '1993-06-12', 2500),
  (N'Xenofon',N'Vlachogiannis', '1989-06-01', 2200),
  (N'Zack',N'Drimiskianakis', '1993-12-11', 3000),
  (N'Eleni',N'Parisi', '1989-06-03', 2400),
  (N'Panagiotis',N'Rizos', '1993-07-02', 2600),
  (N'Alexandros',N'Nomikos', '1973-04-25', 2700),
  (N'Konstantinos',N'Argyropoulos', '1990-08-01', 2800),
  (N'Chris',N'Vasilakis', '1989-04-09', 2900),
  (N'Thanasis',N'Kontodimas', '1991-08-03', 2000),
  (N'Thanos',N'Katrakis', '1988-02-01', 3000);

USE PerikleousBootcamp;
INSERT INTO CourseStudents
VALUES
  (1,1),
  (1,2),
  (1,3),
  (1,4),
  (2,4),
  (2,5),
  (2,6),
  (3,7),
  (3,8),
  (4,9),
  (4,10),
  (2,1),
  (3,1);

USE PerikleousBootcamp;
INSERT INTO CourseTrainers
VALUES
  (1,1),
  (1,2),
  (2,2),
  (2,3),
  (3,3),
  (4,4);
