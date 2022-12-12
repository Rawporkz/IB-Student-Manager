using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IB_Student_Manager.Models
{
	public class StudentManager
	{
		//Generate student object from Ilist of IList
		GGSheet Sheet = new GGSheet();
		static string Page;
		static string FirstCol;
		static string RowNum;
		static string EndCol;
		public List<StudentClass> GenerateStudent()
		{
			Page = "Student";
			FirstCol = "A";
			RowNum = "1";
			EndCol = "X";
			IList<IList<object>> Data = Sheet.LoadData(Page, FirstCol, RowNum, EndCol);
			List<StudentClass> students = new List<StudentClass> { };
			
			foreach (IList<object> IList in Data)
			{
				
				StudentClass student = new StudentClass();
				student.Name = (string)IList[0];
				student.Email = (string)IList[1];
				student.Password = (string)IList[2];
				student.FormGroup = (string)IList[3];
				student.Total = (string)IList[4];
				student.DangerLevel = (string)IList[5];

				int x = 6;
				for(int subjectnumber = 0; subjectnumber < 6; subjectnumber++)
				{
					student.Subjects[subjectnumber].Name = (string)IList[x];
					student.Subjects[subjectnumber].Class = (string)IList[x+1];
					student.Subjects[subjectnumber].Grade = (string)IList[x+2];
					x = x + 3;
					
				}
				students.Add(student);
			}
			return students;
		}
	}
}
