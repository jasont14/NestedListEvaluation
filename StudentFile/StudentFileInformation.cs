/*
 *  Jason Thatcher      March 2018
 *
 *  StudentFileInformation.cs
 *  
 *  Methods for operations on List<Student> 
 *  for application. 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentFile
{
    class StudentFileInformation
    {
        List<Student> studentList = new List<Student>();
        int studentListCount = 0;

        public StudentFileInformation(List<Student> s)
        {
            studentList = s;
            studentListCount = studentList.Count();
        }

        //Returns list<student>
        public List<Student> GetStudentListAll()
        {
            return studentList;
        }

        //Finds students with GPA>3.0.  Depends on Predicate
        public int CountStudentsGPAGreaterThan3_00()
        {
            List<Student> result = studentList.FindAll(Predicate3_00GPA);

            return result.Count();
        }

        //Predicate tells how to filter list for CountStudentsGPAGreaterThan3_00()
        private static bool Predicate3_00GPA(Student s)
        {
            if (s.Gpa > 3.00d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Returns List of Students by LastName
        public List<Student> GetStudentByLastName(string lastName)
        {
            List<Student> result = new List<Student>();

            foreach(Student s in studentList)
            {
                if (s.StudentName.LastName == lastName)
                {
                    result.Add(s);
                }
            }

            return result;

        }

        //Prints list of students and GPA if last name = anderson
        public void PrintNameGPAAnderson()
        {
            Console.WriteLine("Students with Last Name 'Anderson' and GPA");
            Console.WriteLine();
            Console.WriteLine("{0,-25} {1,-15}", "Student Name", "GPA");
            Console.WriteLine("{0,-25} {1,-15}", "============", "===");

            foreach (Student s in studentList)
            {
                if (s.StudentName.LastName == "Anderson")
                {
                    Console.WriteLine("{0,-25} {1,-15}", s.StudentName.ToString(), s.Gpa.ToString("N2"));
                }
            }
        }

        //Returns count of students without email
        public int CountStudentNoEmail()
        {
            int result = 0;
            foreach (Student s in studentList)
            {
                if (s.Email.ToUpper() ==  "NONE")
                {
                    result++;
                }
            }
            return result;
        }

        //Returns average GPA over students
        public double GetAverageGPA()
        {
            double tally = 0.00d;
            double result = 0.0000000d;

            foreach (Student s in studentList)
            {
                tally += s.Gpa;       
            }

            double dCount = Convert.ToDouble(studentList.Count());
            result = tally / dCount;
            return result;

        }

        //Inserts a new student in proper order in orderedlist of students.
        public void AddStudent(Student newStudent)
        {
            int count = 0;

            foreach(Student s in studentList)
            {
                if (String.Compare(s.StudentName.LastName,newStudent.StudentName.LastName) == 1)
                {
                    break;
                }

                count++;
            }

            studentList.Insert(count, newStudent);
        }

        public void PrintStudentList(List<Student> s)
        {
            Console.Write("{0,-30}", "Student Name");
            Console.Write("{0,-20}", "Phone");
            Console.Write("{0,-30}", "Email");
            Console.Write("{0,-10}", "GPA");
            Console.WriteLine();

            foreach (Student st in s)
            {
                Console.Write("{0,-30}", st.StudentName.ToString());
                Console.Write("{0,-20}", st.Phone);
                Console.Write("{0,-30}", st.Email);
                Console.Write("{0,-10}", st.Gpa);
                Console.WriteLine();
            }
        }
    }
}
