
using System.Collections.Generic;
using IB_Student_Manager.Models;

namespace IB_Student_Manager.Models
{
    public class StudentClass : Person
    {
        public string Total { get; set; }
        public string DangerLevel { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject> { new Subject { Name = "", Class = "", Grade = "" }, new Subject { Name = "", Class = "", Grade = "" }, new Subject { Name = "", Class = "", Grade = "" }, new Subject { Name = "", Class = "", Grade = "" }, new Subject { Name = "", Class = "", Grade = "" }, new Subject { Name = "", Class = "", Grade = "" } };

        public List<string> ConvertToLisOfString()
        {
            Password = Hash.ComputeSha256Hash(Password);
            List<string> StudentData = new List<string> { Name, Email,Password, FormGroup, Total, DangerLevel };
            for (int i = 0; i < 6; i++)
            {
                if (Subjects[i].Name != null && Subjects[i].Class != null && Subjects[i].Grade != null)
                {
                    StudentData.Add(Subjects[i].Name);
                    StudentData.Add(Subjects[i].Class);
                    StudentData.Add(Subjects[i].Grade);
                }
                else
                {
                    StudentData.Add(" ");
                    StudentData.Add(" ");
                    StudentData.Add(" ");
                }

            }

            return StudentData;
        }

    }

    
}
