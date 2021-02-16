using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace modified_Student_management_System
{
    public class SchoolManagementService
    {
        public List<School> Schools;
        public static List<School> readData;

        public SchoolManagementService()
        {
            Schools = new List<School>();
            JsonFileRead();
        }

        public async void JsonFileWriteAsync()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(Schools, options);
            await File.AppendAllTextAsync("schools.json", jsonString);
        }

        public void JsonFileRead()
        {
            string jsonString = File.ReadAllText("schools.json");
            if (jsonString.Length != 0)
            {
                readData = JsonSerializer.Deserialize<List<School>>(jsonString);
            }
        }

        public bool IsSchoolExist(string name)
        {
            if (readData!=null)
            {
                return readData.Exists(sch => sch.Name==name);
            }
            return false;
        }

        public int FindSchool(string name)
        {
            return readData.Find(sch => String.Equals(sch.Name, name)).Id;
        }

        public string GetSchoolName(int id)
        {
            var school = Schools.Find(val => val.Id == id);
            return school.Name;
        }

        public void AddStudent(int id, int rollNumber, string name)
        {
            var school = Schools.Find(val => val.Id == id);
            Student student = new Student();
            student.Name = name;
            student.RollNumber = rollNumber;
            school.Students.Add(student);
            //
        }

        public void AddMarksToStudent(int id, int rollNumber, int teluguMarks, int hindiMarks, int englishMarks, int mathsMarks, int scienceMarks, int socialMarks)
        {
            var school = Schools.Find(val => val.Id == id);
            foreach (var roll in school.Students)
            {
                if (roll.RollNumber == rollNumber)
                {
                    roll.ProgressCard.TeluguMarks = teluguMarks;
                    roll.ProgressCard.HindiMarks = hindiMarks;
                    roll.ProgressCard.EnglishMarks = englishMarks;
                    roll.ProgressCard.MathsMarks = mathsMarks;
                    roll.ProgressCard.ScienceMarks = scienceMarks;
                    roll.ProgressCard.SocialMarks = socialMarks;
                    break;
                }
            }
        }

        public Student GetStudent(int id, int rollNumber)
        {
            var school = Schools.Find(val => val.Id == id);
            return school.Students.Find(stu => stu.RollNumber == rollNumber);
        }

        public ProgressCard GetProgressCard(int id, int rollNumber)
        {
            var school = Schools.Find(val => val.Id == id);
            return (school.Students.Find(stu => stu.RollNumber == rollNumber)).ProgressCard;
        }

        public bool IsStudentExist(int id, int rollNumber)
        {
            var school = Schools.Find(val => val.Id == id);
            return school.Students.Exists(stu => stu.RollNumber == rollNumber);
        }

    }
}
