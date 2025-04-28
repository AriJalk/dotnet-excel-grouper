using ClosedXML.Excel;
using ExcelGrouper.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExcelGrouper.DataStructures;

namespace ExcelGrouper
{

	internal class Program
	{

		static void Process(string path, ExcelConfiguration? configuration)
		{
			if (string.IsNullOrEmpty(path) || !string.Equals(Path.GetExtension(path), ".json"))
			{
				Console.WriteLine("Please use a json file as parameter");
				return;
			}

			if (configuration == null)
			{
				Console.WriteLine("Invalid configuration");
				return;
			}
			ExcelContext? context = FileHandler.GetExcelContext(configuration);
			if (context == null)
			{
				Console.WriteLine("Can't open excel file");
				return;
			}

			if (configuration.ProcessOption == null)
			{
				Console.WriteLine("ProcessOption not specified, setting to Synchronous");
				configuration.ProcessOption = ExcelConfiguration.ProcessOptions.SYNCHRONOUS;
			}

			string output = WorkbookHandler.ProcessWorkbook(context);
			FileHandler.WriteFile($"{configuration.PathWithoutExtension}_{configuration.WorksheetName}.txt", output);
		}

		static void Main(string[] args)
		{
			ExcelConfiguration? configuration = null;
			string path;
#if DEBUG
			path = "D:/Users/Ariel/Downloads/חישוב פירמידות.json";
			//path = "D:/Users/Ariel/Downloads/Book1.json";
#else
			path = args[0];
#endif
			try
			{
				configuration = FileHandler.GetExcelConfiguration(path);
				Process(path, configuration);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
			

#if !DEBUG
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
#endif
		}
	}
}
