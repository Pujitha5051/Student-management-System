using System;
using System.Collections.Generic;
using System.Text;

namespace modified_Student_management_System
{
    public class ProgressCard
    {       
        public int TeluguMarks { get; set; }

        public int HindiMarks { get; set; }

        public int EnglishMarks { get; set; }

        public int MathsMarks { get; set; }

        public int ScienceMarks { get; set; }

        public int SocialMarks { get; set; }

        public int TotalMarks
        {
            get
            {
                return this.TeluguMarks + this.EnglishMarks + this.HindiMarks + this.MathsMarks + this.ScienceMarks + this.SocialMarks;
            }
        }

        public float Percentage {          
        get
            {
                var subjectsCount = Enum.GetNames(typeof(EnumClass.Subjects)).Length;
                return (float)this.TotalMarks / subjectsCount;
            }
        }
    }
}
