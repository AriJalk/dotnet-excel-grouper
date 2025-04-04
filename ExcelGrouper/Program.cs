using ClosedXML.Excel;
using ExcelGrouper.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExcelGrouper.DataStructures;

namespace ExcelGrouper
{

	internal class Program
	{

		static void Main(string[] args)
		{
			ExcelConfiguration? configuration = null;
			string path;
#if DEBUG
			path = "D:/Users/Ariel/Downloads/Book1.json";
#else
			path = args[0];
#endif
			if (string.IsNullOrEmpty(path) || !string.Equals(Path.GetExtension(path), ".json"))
			{
				Console.WriteLine("Please use a json file as parameter");
				return;
			}

			configuration = FileHandler.GetExcelConfiguration(path);
			ExcelContext? context = FileHandler.GetExcelContext(configuration);
			if (context == null)
			{
				return;
			}

			string output = WorkbookHandler.ProcessWorkbook(context);
			FileHandler.WriteFile($"{configuration.PathWithoutExtension}_{configuration.WorksheetName}.txt", output);
		}
	}
}
