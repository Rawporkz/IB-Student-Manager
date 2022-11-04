using IB_Student_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IB_Student_Manager.Models;

namespace IB_Student_Manager.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		static string Page;
		static string FirstCol;
		static string RowNum;
		static string EndCol;
		static int[] Rows;
		GGSheet Sheet = new GGSheet();

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult YearGroup()
		{
			Page = "Student";
			FirstCol = "A";
			RowNum = "1";
			EndCol = "X";
			IList<IList<Object>> Data = Sheet.LoadData(Page, FirstCol, RowNum, EndCol);
			ViewData["Data"] = Data;


			//sets data for array to be put into view
			string Day = DateTime.Now.DayOfWeek.ToString();
			ViewData["Day"] = Day;

			return View();
		}

		public IActionResult SeeFriend(string name)
		{

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult StudentInfo(int id)
		{
			//Need to fix not being able to get row from table
			GGSheet Sheet = new GGSheet();
			Page = "Student";
			FirstCol = "A";
			RowNum = "1";
			EndCol = "X";
			IList<IList<Object>> Data = Sheet.LoadData(Page, FirstCol, RowNum, EndCol);
			ViewData["Data"] = Data;
			Rows = new int[1];
			Rows[0] = id;
			ViewData["Row"] = Rows;

			return View();
		}

		public IActionResult ManageStudent()
		{
			return View();
		}

		public IActionResult DoSomething()
		{

			return View(new StudentClass());
		}
		[HttpGet]
		public IActionResult AddStudent()
		{

			return View(new StudentClass());
		}

		[HttpPost]//send data from the form to server
		public IActionResult AddStudent(StudentClass MyClass)
		{
			//add student to db

			List<string> StudentData = MyClass.ConvertToLisOfString();
			Sheet.SaveData(StudentData, "Student");


			return RedirectToAction("StudentInfo");
		}

	}
}
