/*
 *  Jason Thatcher      March 2018
 *
 *  Name.cs
 *  
 *  Class that provides blueprint for Name 
 *  
 */

using System;

namespace StudentFile
{
    class Name
    {
        string firstName;
        string lastName;
        string middleName;
        private int pointer;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }

        public Name()
        {
            FirstName = "";
            LastName = "";
            middleName = "EMPTY";
            pointer = 1;
        }

        //During file ops add as object and handle logic here to accommodate different datatypes
        public void AddFromObject(object ob)
        {
            switch (pointer++)
            {
                case 1:
                    LastName = ob.ToString();
                    break;
                case 2:
                    FirstName = ob.ToString();
                    break;
                case 3:
                    MiddleName = ob.ToString();
                    break;
            }
        }

        public override string ToString()
        {
            return LastName + ", " + FirstName + ", " + MiddleName;
        }
    }
}
