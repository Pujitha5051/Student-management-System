using System;
using System.Collections.Generic;
using System.Text;

namespace modified_Student_management_System
{
    public class Student
    {
        public string Name { get; set; }

        public int RollNumber { get; set; }

        public ProgressCard ProgressCard { get; set; }

        public Student()
        {
            ProgressCard = new ProgressCard();
        }
      
    }
}
