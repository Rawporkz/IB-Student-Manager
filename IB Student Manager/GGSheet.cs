using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace IB_Student_Manager
{
	public class GGSheet
	{
		static SheetsService Service { get; set; }
		static string[] Scopes = { SheetsService.Scope.Spreadsheets };
		static string ApplicationName = "WebApplicaation1";

		static readonly string spreadsheetId = "1EP1gFbeDGxN4qAk5gH54IvGPmiveieXgU9_Zomb8Klk";
		static readonly string sheet = "Sheet1";
		static SheetsService service;

		static GGSheet()
		{


		}

		public IList<IList<Object>> LoadData(string Page, string FirstCol, string RowNum, string EndCol)
		{
			UserCredential credential;
			using (var stream =
					   new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
			{
				/* The file token.json stores the user's access and refresh tokens, and is created
                 automatically when the authorization flow completes for the first time. */
				string credPath = "token.json";
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.FromStream(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);
			}

			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName
			});

			String range = Page + "!" + FirstCol + RowNum + ":" + EndCol;
			SpreadsheetsResource.ValuesResource.GetRequest request =
				service.Spreadsheets.Values.Get(spreadsheetId, range);


			ValueRange response = request.Execute();
			IList<IList<Object>> values = response.Values;
			var Spaces = new List<object>() { " ", " " };
			//sets the number of columns as the number of headings there is
			int NumOfCols = values[0].Count;
			for (int row = 1; row < values.Count; row++)
			{
				if (values[row].Count == 0)
				{
					Spaces = new List<object>() { " ", " " };
				}
				else if (values[row].Count == 1 && values[row][0] != null)
				{
					Spaces = new List<object>() { values[row][0], " " };
				}


				if (values[row].Count != NumOfCols)
				{
					values[row].Add(Spaces);

				}


			}
			return values;
		}



		//Parameters need to be updated to match creation
		public void SaveData(List<string> Data, string SheetName)
		{
			UserCredential credential;
			using (var stream =
					   new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
			{
				/* The file token.json stores the user's access and refresh tokens, and is created
                 automatically when the authorization flow completes for the first time. */
				string credPath = "token.json";
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.FromStream(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);
			}

			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName
			});

			var range = $"{SheetName}!A2:B";
			var valueRange = new ValueRange();
			var objectList = new List<object>() { };
			//For adding new students
			if (SheetName == "Student")
			{
				objectList = new List<object>() { "Hello", "Nihao", "Bye bye", "non", "jk" };
			}
			else
			{
				objectList = new List<object>() { "", "","" ,"" ,""  };
			}
			

			valueRange.Values = new List<IList<object>> { objectList };
			//Saving the object to google sheets
			var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
			appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
			var appendResponse = appendRequest.Execute();
		}
		public void UpdateEntry(string Type, int Count)
		{
			UserCredential credential;
			using (var stream =
					   new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
			{
				/* The file token.json stores the user's access and refresh tokens, and is created
                 automatically when the authorization flow completes for the first time. */
				string credPath = "token.json";
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.FromStream(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);
			}

			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName
			});
			var range = $"{sheet}!A2";
			var valueRange = new ValueRange();

			var objectList = new List<object>() { "Upadated" };
			valueRange.Values = new List<IList<object>> { objectList };
			var updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
			updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
			var appendResponse = updateRequest.Execute();
		}
	}
}
