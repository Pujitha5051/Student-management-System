using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace modified_Student_management_System
{
    class Program
    {
        static int subjectCount = Enum.GetNames(typeof(EnumClass.Subjects)).Length;

        static void NavigateMessage()
        {
            Console.Write("Press enter to navigate to Menu.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
        }

        static bool IsRollNumberValid(string rollNumberString)
        {
            //input student's roll number           
            if (String.IsNullOrEmpty(rollNumberString))
            {
                Console.WriteLine("Field can't be left empty.");
                return false;
            }
            else if (!Helper.IsNumber(rollNumberString))
            {
                Console.WriteLine("Roll number can be a number only.");
                return false;
            }
            return true;
        }

        static void Option(int id, SchoolManagementService schoolManagementService)
        {
            var schoolName = schoolManagementService.GetSchoolName(id);
            Console.WriteLine($"\nWelcome to {schoolName} Student information management\n");
            Console.WriteLine("1. Add Student.");
            Console.WriteLine("2. Add marks for student.");
            Console.WriteLine("3. Show student's progress card.");
            Console.WriteLine("4. Add another school.\n");
            Console.WriteLine("Please provide valid input from menu options :");
            string optionString = Console.ReadLine();

            if (!String.IsNullOrEmpty(optionString))
            {
                if (Helper.IsOptionValid(optionString))
                {
                    int option;
                    Int32.TryParse(optionString, out option);
                    switch (option)
                    {
                        case (int)EnumClass.Cases.one:
                            Console.WriteLine("Enter Student's Roll Number ");
                            string rollNumberString;
                            rollNumberString = Console.ReadLine();

                            if (IsRollNumberValid(rollNumberString))
                            {
                                int rollNumber;
                                Int32.TryParse(rollNumberString, out rollNumber);
                                Console.WriteLine("Enter Student's name :");
                                string name = Console.ReadLine();

                                if (String.IsNullOrEmpty(name))
                                {
                                    Console.WriteLine("Field can't be left empty.");
                                }
                                else if (!Helper.IsString(name))
                                {
                                    Console.WriteLine("Student's name can have alphabets only.");

                                }
                                else if (schoolManagementService.IsStudentExist(id, rollNumber))
                                {
                                    Console.WriteLine("a student with same credentials already exist.");
                                }
                                else
                                {
                                    schoolManagementService.AddStudent(id, rollNumber, name);
                                    Console.WriteLine("Student added successfully");
                                }
                                NavigateMessage();
                                Option(id, schoolManagementService);
                            }
                            break;

                        case (int)EnumClass.Cases.two:
                            Console.WriteLine("Enter Student's Roll Number ");
                            rollNumberString = Console.ReadLine();

                            if (IsRollNumberValid(rollNumberString))
                            {
                                int rollNumber;
                                Int32.TryParse(rollNumberString, out rollNumber);

                                if (schoolManagementService.IsStudentExist(id, rollNumber))
                                {
                                    int[] marksArray = new int[subjectCount];
                                    int count = 0;

                                    foreach (var sub in Enum.GetNames(typeof(EnumClass.Subjects)))
                                    {
                                        Console.WriteLine($"Enter marks scored in {sub} :");
                                        string marksString = Console.ReadLine();

                                        if (String.IsNullOrEmpty(marksString))
                                        {
                                            Console.WriteLine("Marks field can't be left empty.");
                                            break;
                                        }
                                        else
                                        {
                                            if (!Helper.IsNumber(marksString))
                                            {
                                                Console.WriteLine("Marks can be a number only.");
                                                break;
                                            }
                                            else
                                            {
                                                if (!Helper.IsMarksValid(marksString))
                                                {
                                                    Console.WriteLine("Invalid marks.");
                                                    break;
                                                }
                                                else
                                                {
                                                    int marksInt;
                                                    Int32.TryParse(marksString, out marksInt);
                                                    marksArray[count++] = marksInt;
                                                }
                                            }
                                        }
                                    }
                                    if (count == subjectCount)
                                    {
                                        schoolManagementService.AddMarksToStudent(id, rollNumber, marksArray[0], marksArray[1], marksArray[2], marksArray[3], marksArray[4], marksArray[5]);
                                        Console.WriteLine("Marks added Successfully");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Roll number doesn't exist");
                                }
                            }
                            NavigateMessage();
                            Option(id, schoolManagementService);
                            break;

                        case (int)EnumClass.Cases.three:
                            Console.WriteLine("Enter Student's Roll Number ");
                            rollNumberString = Console.ReadLine();

                            if (IsRollNumberValid(rollNumberString))
                            {
                                int rollNumber;
                                Int32.TryParse(rollNumberString, out rollNumber);
                                if (schoolManagementService.IsStudentExist(id, rollNumber))
                                {
                                    Student student = schoolManagementService.GetStudent(id, rollNumber);
                                    Console.WriteLine("Student's Roll Number : " + student.RollNumber);
                                    Console.WriteLine("Student's Name : " + student.Name + "\n");
                                    Console.WriteLine("Student's Marks");

                                    //display telugu marks.
                                    Console.WriteLine("Telugu :" + student.ProgressCard.TeluguMarks);

                                    //display telugu marks.
                                    Console.WriteLine("Hindi :" + student.ProgressCard.HindiMarks);

                                    //display english marks.
                                    Console.WriteLine("English :" + student.ProgressCard.EnglishMarks);

                                    //display maths marks.
                                    Console.WriteLine("Maths :" + student.ProgressCard.MathsMarks);

                                    //display Science marks.
                                    Console.WriteLine("Science :" + student.ProgressCard.ScienceMarks);

                                    //display Social marks.
                                    Console.WriteLine("Social :" + student.ProgressCard.SocialMarks + "\n");

                                    //display total marks.

                                    Console.WriteLine("Total Marks :" + student.ProgressCard.TotalMarks);

                                    //display Percentage upto 2 decimal points.
                                    Console.WriteLine("Percentage :" + Math.Round(student.ProgressCard.Percentage, 2) + " %");
                                }

                                else
                                {
                                    Console.WriteLine("Roll number doesn't exist");
                                }
                            }
                            break;

                        case (int)EnumClass.Cases.four:
                            NavigateMessage();
                            Console.WriteLine("Add another school.");
                            Start(id, schoolManagementService);
                            break;

                        default:
                            Console.WriteLine("Please select a valid option from the menu");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Select a valid option.");
                }
            }
            else
            {
                Console.WriteLine("Field can't be left empty.");
            }
            NavigateMessage();
            Option(id, schoolManagementService);
        }

        static void Start(int id, SchoolManagementService schoolManagementService)
        {
            Console.WriteLine("Enter School Name :");
            string schoolName = Console.ReadLine();

            if (String.IsNullOrEmpty(schoolName))
            {
                Console.WriteLine("School name can't be left blank.");
                NavigateMessage();
                Start(id, schoolManagementService);
            }
            else if (!Helper.IsString(schoolName))
            {
                Console.WriteLine("School name can have only alphabets");
                NavigateMessage();
                Start(id, schoolManagementService);
            }
            else
            {
                if (!schoolManagementService.IsSchoolExist(schoolName))
                {
                    Console.WriteLine("School doesn't exist, add a school.");
                    schoolName = Console.ReadLine();
                    School school = new School();
                    school.Id = ++id;
                    school.Name = schoolName;
                    schoolManagementService.Schools.Add(school);
                }
                else
                {
                    id = schoolManagementService.FindSchool(schoolName);
                }
                schoolManagementService.JsonFileWriteAsync();
                Option(id, schoolManagementService);
            }
        }

        static void Main(string[] args)
        {
            //trigger start function to start the application.
            int id = 0;
            SchoolManagementService schoolManagementService = new SchoolManagementService();
            Start(id, schoolManagementService);
        }
    }
}
