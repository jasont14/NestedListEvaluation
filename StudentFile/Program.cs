/*
 *  Jason Thatcher      March 2018
 *
 *  Program.cs
 *  
 *  Entry point to application.
 *  Loads student text file, evaluates
 *  and reports on text file, inserts
 *  student and reports again, and finally
 *  writes changes to file.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //set textfile name
            string file = "students.txt";
            StudentFile studentFile = new StudentFile(file);
            List<Student> myStudents =  studentFile.CreateListFromFile();

            Console.WriteLine("**************************");
            Console.WriteLine("* BEFORE INSERSTION      *");
            Console.WriteLine("**************************");

            StudentFileInformation studentInfo = new StudentFileInformation(myStudents);
            Console.WriteLine("Students with GPA > 3.0: {0}", studentInfo.CountStudentsGPAGreaterThan3_00().ToString());
            Console.WriteLine("Students without email: {0}", studentInfo.CountStudentNoEmail().ToString());
            Console.WriteLine("Average GPA: {0}", studentInfo.GetAverageGPA().ToString("N6"));
            Console.WriteLine("Total Student Count: {0}", myStudents.Count());
            studentInfo.PrintNameGPAAnderson();

            Console.WriteLine();
            Console.WriteLine("**************************");
            Console.WriteLine("* INSERSTION             *");
            Console.WriteLine("**************************");

            Name malName = new Name();
            malName.FirstName = "Malachi";
            malName.LastName = "Constant";
            malName.MiddleName = "";

            Student malachi = new Student();
            malachi.StudentName = malName;
            malachi.Phone = "5558675309";
            malachi.Email = "mconstant@yahoo.com";
            malachi.Gpa = 4.00d;

            studentInfo.AddStudent(malachi);

            Console.WriteLine();
            Console.WriteLine("**************************");
            Console.WriteLine("* AFTER INSERSTION       *");
            Console.WriteLine("**************************");

            StudentFileInformation newStudentInfo = new StudentFileInformation(myStudents);
            Console.WriteLine("Students with GPA > 3.0: {0}", newStudentInfo.CountStudentsGPAGreaterThan3_00().ToString());
            Console.WriteLine("Students without email: {0}", newStudentInfo.CountStudentNoEmail().ToString());
            Console.WriteLine("Average GPA: {0}", newStudentInfo.GetAverageGPA().ToString("N6"));
            Console.WriteLine("Total Student Count: {0}", myStudents.Count());
            newStudentInfo.PrintNameGPAAnderson();

            Console.WriteLine();
            Console.WriteLine("**************************");
            Console.WriteLine("* WRITE TO FILE          *");
            Console.WriteLine("**************************");

            studentFile.CreateFileFromList(studentInfo.GetStudentListAll());
            Console.WriteLine("SUCCESS!  \r\nWRITTEN TO {0}: ", studentFile.OutputFile);

            Console.ReadKey();
        }

    }
}
