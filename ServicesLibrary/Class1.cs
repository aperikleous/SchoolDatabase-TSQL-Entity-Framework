using System;
using System.Collections.Generic;
using System.Linq;
using EntityDataModel;

namespace ServicesLibrary
{
    public class Services
    {
        /// <summary>
        /// Prints the database letters. Used in the initial and ending application screen
        /// </summary>
        /// <param name="color">If color is green, the database letters are displayed in green. This is the initial database screen. 
        /// If color is red, the database letters are displayed in red. This is the ending database screen.</param>
        public static void DisplayDatabaseScreen(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("****************************************************************************************************************************************");
            Console.WriteLine("****************************************************************************************************************************************");
            Console.WriteLine("*****                                                                                                                              *****");
            Console.WriteLine("*****  *********            ****         **********         ****           *******            ****         *********    *********  *****");
            Console.WriteLine("*****  **********          ******        **********        ******          ********          ******        *********    *********  *****");
            Console.WriteLine("*****  ***    ****        ***  ***          ****          ***  ***         **    ***        ***  ***       ***          ***        *****");
            Console.WriteLine("*****  ***    *****      ***    ***         ****         ***    ***        ********        ***    ***      *********    *********  *****");
            Console.WriteLine("*****  ***    *****     ************        ****        ************       ********       ************     *********    *********  *****");
            Console.WriteLine("*****  ***    ****     **************       ****       **************      **    ***     **************          ***    ***        *****");
            Console.WriteLine("*****  **********     ***          ***      ****      ***          ***     ********     ***          ***   *********    *********  *****");
            Console.WriteLine("*****  *********     ***            ***     ****     ***            ***    *******     ***            ***  *********    *********  *****");
            Console.WriteLine("*****                                                                                                                              *****");
            Console.WriteLine("****************************************************************************************************************************************");
            Console.WriteLine("****************************************************************************************************************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Retrieves a valid int value from the user
        /// </summary>
        /// <returns>The integer typed in by the user</returns>
        public static int IntegerInput()
        {
            int input = 0;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
            }
            return input;
        }
        /// <summary>
        /// Retrieves a string by the user
        /// </summary>
        /// <returns>The string typed in by the user</returns>
        public static string StringInput()
        {
            string input = Console.ReadLine();
            return input;
        }
        /// <summary>
        /// Retrieves a DateTime value by the user in the correct format
        /// </summary>
        /// <returns>The date typed in by the user</returns>
        public static DateTime DateInput()
        {
            DateTime input = new DateTime();
            while (!DateTime.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please Enter a valid date!");
            }
            return input;
        }
        /// <summary>
        /// Checks whether the user has typed in the exit character "N" or "n" to escape an input loop
        /// </summary>
        /// <returns>Returns true if the user wants to escape an input loop and false if they want to continue in the loop</returns>
        public static bool ExitCheck()
        {
            if (StringInput().ToUpper() == "N")
                return true;
            else
                return false;
        }
        /// <summary>
        /// Checks whether the user input is within the numerical range of the available options
        /// </summary>
        /// <param name="input">The input that was typed in last by the user</param>
        /// <param name="range">The numerical range of the available options to restrict the user</param>
        /// <returns></returns>
        public static bool CheckRange(int input, int range)
        {
            if ((input > 0) && (input <= range))
            {
                return true;
            }
            else
            {
                Console.WriteLine("This number does not correspond to any of the options above");
                return false;
            }

        }
        /// <summary>
        /// Checks whether the user tries to assign to the same entity twice.If all checks are valid, the current input is placed in the items list along with the previous user inputs
        /// </summary>
        /// <param name="items">The list of the previous values the user has typed in to check against their current input</param>
        /// <param name="currentinput">The input that was typed in by the user</param>
        /// <param name="range">The numerical range of the available options to restrict the user</param>
        public static void CheckExistsAndAdd(List<int> items, int currentinput, int range)
        {
            if (items.Exists(item => item.Equals(currentinput)))
            {
                Console.WriteLine("You have already typed this in! Type another one");
                return;
            }
            if (CheckRange(currentinput, range))
            {
                items.Add(currentinput);
            }
        }
        /// <summary>
        /// Overloaded function. Prints the available courses and checks which courses the user wants to assign to. Used for the many-to-many relationships
        /// </summary>
        /// <param name="items">The list of the previous values the user has typed in to check against their current input</param>
        public static void CourseMatch(List<int> items)
        {
            int range = 0;
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                range = db.Courses.Count();
            }
            do
            {
                PrintAllCourses();
                Console.WriteLine("Please type in the number that corresponds to the respective course. Any numbers above or below that range will not be accepted.");
                int input = IntegerInput();
                CheckExistsAndAdd(items, input, range);
                Console.WriteLine("Would you like to stop adding courses? Type N or n if you want to stop or any other character to continue");
            } while (!ExitCheck());
        }
        /// <summary>
        /// Overloaded function. Prints the available courses and checks which course the user wants to assign to. Used for the one-to-many relationships
        /// </summary>
        /// <returns>The courseId of the course the user wants to assign to</returns>
        public static int CourseMatch()
        {
            int range = 0;
            int input = 0;
            bool Matched = false;
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                range = db.Courses.Count();
            }
            do
            {
                PrintAllCourses();
                Console.WriteLine("Please type in the number of the course you want to assign to. An assignment can be assigned to only one course.");
                Console.WriteLine("Any numbers above or below that range will not be accepted.");
                input = IntegerInput();
                Matched = CheckRange(input, range);
            } while (!Matched);
            return input;
        }
        /// <summary>
        /// Creates assignments submitted by the user and adds them to the assignments in the database.
        /// Once the user has no more assignments to submit, they are returned to the input menu.
        /// </summary>
        public static void AssignmentInput()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please type in the title of the assignment");
                string titleinput = StringInput();
                Console.WriteLine("Please type in the description of the assignment");
                string descriptioninput = StringInput();
                Console.WriteLine("Please type in the submission date of the assignment(format : dd/mm/yyyy)");
                DateTime subDate = DateInput();
                Console.WriteLine("Please type in the oral mark of the assignment");
                int oralmark = IntegerInput();
                Console.WriteLine("Please type in the total mark of the assignment");
                int totalmark = IntegerInput();
                int correspondingCourse = CourseMatch();
                Assignments a = new Assignments() { title = titleinput, description = descriptioninput, subDateTime = subDate, oralMark = oralmark, totalMark = totalmark, courseId = correspondingCourse };
                using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
                {
                    db.Assignments.Add(a);
                    db.SaveChanges();
                }
                Console.WriteLine();
                Console.WriteLine("Great work! A new assignment has been added to the database.");
                Console.WriteLine("Do you want to continue adding assignments? Type N or n if you want to return to the previous menu or any other button to continue adding");
            } while (!ExitCheck());
            InputMenu();
        }
        /// <summary>
        /// Creates courses submitted by the user and adds them to the courses in the database.
        /// Once the user has no more courses to submit, they are returned to the input menu.
        /// </summary>
        public static void CourseInput()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please type in the title of the course");
                string titleinput = StringInput();
                Console.WriteLine("Please type in the stream of the course");
                string streaminput = StringInput();
                Console.WriteLine("Please type in the type of the course");
                string typeinput = StringInput();
                Console.WriteLine("Please type in the start date of the course (format : dd/mm/yyyy)");
                DateTime startDate = DateInput();
                Console.WriteLine("Please type in the end date of the course (format : dd/mm/yyyy)");
                DateTime endDate = DateInput();
                Courses c = new Courses() { title = titleinput, stream = streaminput, type = typeinput, start_date = startDate, end_date = endDate };
                using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
                {
                    db.Courses.Add(c);
                    db.SaveChanges();
                }
                Console.WriteLine();
                Console.WriteLine("Great work! A new course has been added to the database.");
                Console.WriteLine("Do you want to continue adding courses? Type N or n if you want to return to the previous menu or any other button to continue adding");
            } while (!ExitCheck());
            InputMenu();
        }
        /// <summary>
        /// Creates trainers submitted by the user and adds them to the trainers in the database.
        /// Once the user has no more trainers to submit, they are returned to the input menu.
        /// </summary>
        public static void TrainerInput()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please type in the first name of the trainer");
                string firstname = StringInput();
                Console.WriteLine("Please type in the last name of the trainer");
                string lastname = StringInput();
                Console.WriteLine("Please type in the subject of the trainer");
                string subj = StringInput();
                List<int> courses = new List<int>();
                Console.WriteLine("Does this trainer teach any courses? If yes, type any character to start adding courses. If no, type n or N");
                if (!ExitCheck())
                    CourseMatch(courses);
                Trainers t = new Trainers() { firstName = firstname, lastName = lastname, subject = subj };
                using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
                {
                    for (int i = 0; i < courses.Count; i++)
                    {
                        int currentcourse = courses[i];
                        foreach (var item in db.Courses.Where(x => x.courseId == currentcourse))
                        {
                            t.Courses.Add(item);
                        }
                    }
                    db.Trainers.Add(t);
                    db.SaveChanges();
                }
                Console.WriteLine();
                Console.WriteLine("Great work! A new trainer has been added to the database.");
                Console.WriteLine("Do you want to continue adding trainers? Type N or n if you want to return to the previous menu or any other button to continue adding");
            } while (!ExitCheck());
            InputMenu();
        }
        /// <summary>
        /// Creates students submitted by the user and adds them to the students in the database.
        /// Once the user has no more students to submit, they are returned to the input menu.
        /// </summary>
        public static void StudentInput()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please type in the first name of the student");
                string firstname = StringInput();
                Console.WriteLine("Please type in the last name of the student");
                string lastname = StringInput();
                Console.WriteLine("Please type in the student's date of birth (format : dd/mm/yyyy)");
                DateTime dateofbirth = DateInput();
                Console.WriteLine("Please type in the student's tuition fees");
                int tuitionfees = IntegerInput();
                List<int> courses = new List<int>();
                Console.WriteLine("Does this student attend any courses? If yes, type any character to start adding courses. If no, type n or N");
                if (!ExitCheck())
                    CourseMatch(courses);
                Students s = new Students() { firstName = firstname, lastName = lastname, dateOfBirth = dateofbirth, tuitionFees = tuitionfees };
                using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
                {
                    for (int i = 0; i < courses.Count; i++)
                    {
                        int currentcourse = courses[i];
                        foreach (var item in db.Courses.Where(x => x.courseId == currentcourse))
                        {
                            s.Courses.Add(item);
                        }
                    }
                    db.Students.Add(s);
                    db.SaveChanges();
                }
                Console.WriteLine();
                Console.WriteLine("Great work! A new student has been added to the database.");
                Console.WriteLine("Do you want to continue adding students? Type N or n if you want to return to the previous menu or any other button to continue adding");
            } while (!ExitCheck());
            InputMenu();
        }
        /// <summary>
        /// Prints the input menu with all the available user options for insertion
        /// </summary>
        public static void InputMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Choose the entity you want to create from the list below:");
            Console.WriteLine("\t 1 - to add a new student");
            Console.WriteLine("\t 2 - to add a new trainer");
            Console.WriteLine("\t 3 - to add a new assignment");
            Console.WriteLine("\t 4 - to add a new course");
            Console.WriteLine("\t 5 - to access the database");
            Console.WriteLine("\t 6 - to return to the main menu");
            Console.WriteLine("\t 7 - to exit the program");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            int input = 0;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please Enter a valid numerical value between 1 and 7!");
            }
            if (input == 1)
            {
                StudentInput();
            }
            else if (input == 2)
            {
                TrainerInput();
            }
            else if (input == 3)
            {
                AssignmentInput();
            }
            else if (input == 4)
            {
                CourseInput();
            }
            else if (input == 5)
            {
                ServicesMenu();
            }
            else if (input == 6)
            {
                MainMenu();
            }
            else if (input == 7)
            {
                DisplayDatabaseScreen(ConsoleColor.Red);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid number. Maybe you would like to take a look at the options again?");
                InputMenu();
            }
        }
        /// <summary>
        /// Prints the services menu with all the available options for viewing the database
        /// </summary>
        public static void ServicesMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("The database offers the following services:");
            Console.WriteLine("\t 1 - to get all students");
            Console.WriteLine("\t 2 - to get all trainers");
            Console.WriteLine("\t 3 - to get all assignments");
            Console.WriteLine("\t 4 - to get all courses");
            Console.WriteLine("\t 5 - to get all students per course");
            Console.WriteLine("\t 6 - to get all trainers per course");
            Console.WriteLine("\t 7 - to get all assignments per course");
            Console.WriteLine("\t 8 - to get all assignments per course per student");
            Console.WriteLine("\t 9 - to get a list of students that belong to more than one courses");
            Console.WriteLine("\t 10 - to return to the main menu");
            Console.WriteLine("\t 11 - to exit the program");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            int input = 0;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please Enter a valid numerical value between 1 and 12!");
            }
            if (input == 1)
            {
                PrintAllStudents();
                ServicesMenu();
            }
            else if (input == 2)
            {
                PrintAllTrainers();
                ServicesMenu();
            }
            else if (input == 3)
            {
                PrintAllAssignments();
                ServicesMenu();
            }
            else if (input == 4)
            {
                PrintAllCourses();
                ServicesMenu();
            }
            else if (input == 5)
            {
                PrintAllStudentsPerCourse();
                ServicesMenu();
            }
            else if (input == 6)
            {
                PrintAllTrainersPerCourse();
                ServicesMenu();
            }
            else if (input == 7)
            {
                PrintAllAssignmentsPerCourse();
                ServicesMenu();
            }
            else if (input == 8)
            {
                PrintAllAssignmentsPerCoursePerStudent();
                ServicesMenu();
            }
            else if (input == 9)
            {
                PrintAllStudentsInMultipleCourses();
                ServicesMenu();
            }
            else if (input == 10)
            {
                MainMenu();
            }
            else if (input == 11)
            {
                DisplayDatabaseScreen(ConsoleColor.Red);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid number. Maybe you would like to take a look at the options again?");
                ServicesMenu();
            }
        }
        /// <summary>
        /// Main menu that is initially displayed on screen. The user may choose whether they want to view the database or type in their own data
        /// </summary>
        public static void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the school database! Would you like to view the database or would you rather type in your own input?");
            Console.WriteLine("Press:");
            Console.WriteLine("\t 1 - to view the database");
            Console.WriteLine("\t 2 - to provide your own input");
            Console.WriteLine("\t 3 - to exit the program");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            int input = 0;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Please Enter a valid numerical value between 1 and 3!");
            }
            if (input == 1)
            {
                ServicesMenu();
            }
            else if (input == 2)
            {
                InputMenu();
            }
            else if (input == 3)
            {
                DisplayDatabaseScreen(ConsoleColor.Red);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid number. Maybe you would like to take a look at the options again?");
                MainMenu();
            }
        }
        /// <summary>
        /// Initial function that invokes the main menu of the application
        /// </summary>
        public void Start()
        {
            DisplayDatabaseScreen(ConsoleColor.Green);
            MainMenu();

        }
        /// <summary>
        /// Prints all students in the database
        /// </summary>
        public static void PrintAllStudents()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Students)
                {
                    Console.WriteLine(item.firstName + " " + item.lastName + " " + item.dateOfBirth + " " + item.tuitionFees);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all trainers in the database
        /// </summary>
        public static void PrintAllTrainers()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Trainers)
                {
                    Console.WriteLine(item.firstName + " " + item.lastName + " " + item.subject);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all assignments in the database
        /// </summary>
        public static void PrintAllAssignments()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Assignments)
                {
                    Console.WriteLine(item.title + " " + item.description + " " + item.subDateTime + " " + item.oralMark + " " + item.totalMark);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all courses in the database
        /// </summary>
        public static void PrintAllCourses()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Courses)
                {
                    Console.WriteLine(item.courseId + " " + item.title + " " + item.stream + " " + item.type + " " + item.start_date + " " + item.end_date);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all students per course in the database
        /// </summary>
        public static void PrintAllStudentsPerCourse()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Courses)
                {
                    Console.WriteLine(item.title + " " + item.stream + " " + item.type);
                    foreach (var s in item.Students)
                    {
                        Console.Write("\t");
                        Console.WriteLine(s.firstName + " " + s.lastName);
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all trainers per course in the database
        /// </summary>
        public static void PrintAllTrainersPerCourse()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Courses)
                {
                    Console.WriteLine(item.title + " " + item.stream + " " + item.type);
                    foreach (var t in item.Trainers)
                    {
                        Console.Write("\t");
                        Console.WriteLine(t.firstName + " " + t.lastName);
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all assignments per course in the database
        /// </summary>
        public static void PrintAllAssignmentsPerCourse()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Courses)
                {
                    Console.WriteLine(item.title + " " + item.stream + " " + item.type);
                    foreach (var a in item.Assignments)
                    {
                        Console.Write("\t");
                        Console.WriteLine(a.title + " " + a.description);
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all assignments per course per student in the database
        /// </summary>
        public static void PrintAllAssignmentsPerCoursePerStudent()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Students)
                {
                    Console.WriteLine(item.firstName + " " + item.lastName);
                    foreach (var c in item.Courses)
                    {
                        Console.Write("\t");
                        Console.WriteLine(c.title + " " + c.stream + " " + c.type);
                        foreach (var a in c.Assignments)
                        {
                            Console.Write("\t\t");
                            Console.WriteLine(a.title + " " + a.description);
                        }
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// Prints all students that attend multiple courses in the database
        /// </summary>
        public static void PrintAllStudentsInMultipleCourses()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            using (PerikleousBootcampEntities db = new PerikleousBootcampEntities())
            {
                foreach (var item in db.Students)
                {
                    if (item.Courses.Count > 1)
                    {
                        Console.WriteLine(item.firstName + " " + item.lastName + " participates in " + item.Courses.Count + " Courses");
                        foreach (var c in item.Courses)
                        {
                            Console.Write("\t");
                            Console.WriteLine(c.title + " " + c.stream + " " + c.type);
                        }
                    }
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
    }
}

