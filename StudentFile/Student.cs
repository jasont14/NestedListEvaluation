/*
 *  Jason Thatcher      March 2018
 *
 *  Student.cs
 *  
 *  Class that provides blueprint for Student
 *  
 */

using System;

namespace StudentFile
{
    class Student
    {
        Name name = new Name();
        string phone = null;
        string email = null;
        double gpa = double.NaN;
        int pointer = 1;

        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public double Gpa { get => gpa; set => gpa = value; }
        public Name StudentName { get => name; set => name = value; }

        public Student()
        {

        }

        //During file ops add as object and handle logic here to accommodate different datatypes
        public void AddFromObject(object ob)
        {
            switch (pointer++)
            {
                case 1:
                    Phone = ob.ToString();
                    break;
                case 2:
                    Email = ob.ToString();
                    break;
                case 3:
                    Gpa = Convert.ToDouble(ob);
                    break;
            }
        }
    }
}
