--A list of all the students
use PerikleousBootcamp;
select * from Students

--A list of all the trainers
use PerikleousBootcamp;
select * from Trainers

--A list of all the assignments
use PerikleousBootcamp;
select * from Assignments

--A list of all the courses
use PerikleousBootcamp;
select * from Courses

--All the students per course
use PerikleousBootcamp;
select
title, stream, type, start_date, end_date, firstName, lastName, dateOfBirth, tuitionFees
from CourseStudents CS
inner join Students S on S.studentId = CS.studentId
inner join Courses C on CS.courseId = C.courseId

--All the trainers per course
use PerikleousBootcamp;
select
title, stream, type, start_date, end_date, firstName, lastName, subject
from CourseTrainers CT
inner join Trainers T on T.trainerId = CT.trainerId
inner join Courses C on C.courseId = CT.courseId

--All the assignments per course
use PerikleousBootcamp;
select
C.title, stream, type, start_date, end_date, A.title, description,subDateTime, oralMark, totalMark
from Courses C
inner join Assignments A on C.courseId = A.courseId

--All the assignments per course per student
use PerikleousBootcamp;
select
firstName, lastName, C.title as Course, A.title as Assignment
from CourseStudents CS
inner join Courses C on CS.courseId = C.courseId
inner join Students S on S.studentId = CS.studentId
inner join Assignments A on C.courseId = A.courseId
GROUP BY firstName, lastName, C.title, A.title

--A list of students that belong to more than one courses
use PerikleousBootcamp;
select
firstName, lastName, count (*) as total_courses
from CourseStudents CS
inner join Students S on S.studentId = CS.studentId
inner join Courses C on CS.courseId = C.courseId
group by firstName,lastName
having (COUNT(*) > 1)
order by total_courses DESC
