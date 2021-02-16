using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace modified_Student_management_System
{
    public static class Helper
    {
        public static bool IsString(string name)
        {
            return !(name.Any(char.IsDigit));
        }

        public static bool IsMarksValid(string marksString)
        {
            int marks;
            Int32.TryParse(marksString,out marks);
            return !(marks > 100 || marks < 0);
        }

        public static bool IsNumber(string RollNumberString)
        {
            Regex r = new Regex("^[0-9]+$");
            return r.IsMatch(RollNumberString);
        }

        public static bool IsOptionValid(string optionString)
        {
            if (!IsNumber(optionString))
                return false;

            List<int> optionList = new List<int> {1,2,3,4};
            int option;
            Int32.TryParse(optionString, out option);
            return optionList.Contains(option);
        }
    }
}


