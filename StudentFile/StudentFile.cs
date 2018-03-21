/*
 *  Jason Thatcher      March 2018
 *
 *  StudentFile.cs
 *  
 *  Methods for file operations for application. 
 *  Reads and Writes to file.
 *  
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StudentFile
{
    class StudentFile
    {
        string file;        
        string listName;
        string outputFile;

        public string ListName { get => listName; }
        public string OutputFile
        {
            get
            {
                DirectoryInfo di = new DirectoryInfo(@".\");
                string direc = di.FullName;
                return direc + outputFile;
            }
        }

        public StudentFile(string filename)
        {
            //"Students.txt"
            file = filename;
        }

        //Reads through file passed above 
        //Uses delimiter and list start / end to parse lists in file.  
        //Returns List<Student>
        public List<Student> CreateListFromFile()
        {
            List<Student> students      = new List<Student>();
            StringBuilder stringEval    = new StringBuilder();
            List<Student> studentList   = new List<Student>();
            List<object> tempStudent    = new List<object>();
            List<object> tempName       = new List<object>();
            Student currentStudent      = new Student();
            Name currentName            = new Name();

            char delimiter              = ' ';
            char listStart              = '(';
            char listEnd                = ')';

            bool skip                   = false;
            bool fileStartFound         = false;
            bool studentFound           = false;
            bool nameFound              = false;
            bool delimiterFound         = false;
           
            string inLine;

            if (File.Exists(file))
            {
                try
                {
                    //'using' handles garbage collection.  Wrapped in try/catch to captuer errors and print e.Message
                    using (StreamReader inFile = new StreamReader(file))
                    {

                        while ((inLine = inFile.ReadLine()) != null)
                        {
                            ParseLine(ref students
                                , inLine
                                , ref skip
                                , ref fileStartFound
                                , ref studentFound
                                , ref nameFound
                                , ref delimiterFound
                                , ref stringEval
                                , ref studentList
                                , ref tempStudent
                                , ref tempName
                                , ref currentStudent
                                , ref currentName
                                , ref delimiter
                                , ref listStart
                                , ref listEnd
                                );
                        }
                    }
                }
                catch (System.IO.IOException exc)
                {
                    Console.WriteLine("Error: " + exc.Message);
                }
            }

            return students;

        }


        //Handles parsing of line. Perhaps 'List' could be contained over more than one line.
        //write to list could be done after parsing several lines if necessary.
        //Alternatively could write several before parsing next line. Dependent upon delimiter and liststart / listend
        private void ParseLine(ref List<Student> students
                            , string line
                            , ref bool skip
                            , ref bool fileStartFound
                            , ref bool studentFound
                            , ref bool nameFound
                            , ref bool delimiterFound
                            , ref StringBuilder stringEval
                            , ref List<Student> studentList
                            , ref List<object> tempStudent
                            , ref  List<object> tempName
                            , ref Student currentStudent
                            , ref Name currentName
                            , ref char delimiter
                            , ref char listStart
                            , ref char listEnd
                            )
        {
            foreach (char c in line)
            {
                string eval = "";

                /*listStart identifies a new list. Student file has three lists
                 *  -> Students (List 1 - fileStart)
                 *      -> Student (List 2 - studentFound)
                 *            ->Name (List 3 - nameFound)
                 *          -phone
                 *          -email
                 *          -gpa
                 */          

                if (c == listStart)
                {
                    if (!fileStartFound)
                    {
                        fileStartFound = true;
                    }
                    else if (!studentFound)
                    {
                        studentFound = true;
                    }
                    else if (!nameFound)
                    {
                        nameFound = true;
                    }
                }

                /*listEnd identifies end of list. Should close in order 
                 *  -> Name (List 3 - nameFound), then
                 *      -> Student (List 2 - studentFound), then
                 *            ->  Students (List 1 - fileStart) 
                 */

                if (c == listEnd)
                {
                    if (nameFound)
                    {
                        //ends Name List
                        nameFound = false;
                    }
                        //ends Student List if Name List Closed & Writes Name, Student and adds to List<Student> students
                    else if (studentFound)
                    {
                        studentFound = false;
                        
                        foreach (object ob in tempName)
                        {
                            currentName.AddFromObject(ob);
                        }

                        tempName = new List<object>();

                        foreach (object ob in tempStudent)
                        {
                            currentStudent.AddFromObject(ob);
                        }

                        tempStudent = new List<object>();

                        //Add
                        currentStudent.StudentName = currentName;
                        students.Add(currentStudent);

                        //Reset
                        currentName = new Name();
                        currentStudent = new Student();
                    }
                    else if (fileStartFound)
                    {
                        fileStartFound = false;
                    }
                }

                if (c == delimiter)
                {
                    delimiterFound = true;
                    //skip - will result in char not being written.
                    skip = true;
                }
                else if (c == listStart || c == listEnd)
                {
                    skip = true;
                    delimiterFound = false;
                }
                else if (!skip)
                {
                    //appends string with char, skips '
                    if (c != '\u0027')
                    {
                        stringEval.Append(c);
                    }
                }

                if (stringEval.ToString().Equals("LIST"))
                {
                    stringEval.Clear();
                }

                //delimiter hit.  Write current string.
                if (delimiterFound && !string.IsNullOrEmpty(stringEval.ToString()) && !stringEval.ToString().Equals("LIST"))
                {
                    eval = stringEval.ToString();
                    stringEval.Clear();

                    //Write to name object. Used List<Object> to allow for mixed datatypes.  mapped once added to actual Name object
                    if (delimiterFound && nameFound)
                    {
                        tempName.Add(eval);
                    }

                    //Write to student object. Used List<Object> to allow for mixed datatypes.  mapped once be sorted once added to actual student object
                    else if (delimiterFound && studentFound)
                    {
                        tempStudent.Add(eval);
                    }
                }
                if (delimiterFound && !fileStartFound && !string.IsNullOrEmpty(eval))
                {
                    listName = eval + " ";
                }

                skip = false;
                delimiterFound = false;
            }
        }

        //writes students to file
        public void CreateFileFromList(List<Student> s)
        {
            outputFile = "Output.txt";

            try
            {
                //using handles garbage collection.  Try/Catch used to cpature errors and report e.Message.
                using (StreamWriter sw = new StreamWriter(outputFile))
                {
                    sw.WriteLine(" " + ListName + "(LIST");

                    char a = '\u0027';
                    foreach (Student st in s)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("(LIST (LIST");

                        sb.Append(" ");
                        sb.Append(a);
                        sb.Append(st.StudentName.LastName);
                        sb.Append(" ");
                        sb.Append(a);
                        sb.Append(st.StudentName.FirstName);
                        sb.Append(" ");
                        sb.Append(a);
                        sb.Append(st.StudentName.MiddleName);
                        sb.Append(" ");
                        sb.Append(")");
                        sb.Append(" ");
                        sb.Append(a);
                        sb.Append(st.Phone);
                        sb.Append(" ");
                        sb.Append(a);
                        sb.Append(st.Email);
                        sb.Append(" ");
                        sb.Append(st.Gpa.ToString());
                        sb.Append(" ");
                        sb.Append(")");

                        sw.WriteLine(sb.ToString());
                    }

                    sw.WriteLine(") )");
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }   
}
