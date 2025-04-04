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
#if DEBUG
			string path = "D:/Users/Ariel/Downloads/Book1.json";
			configuration = FileHandler.GetExcelConfiguration(path);
#else
			if (string.IsNullOrEmpty(args[0]))
			{
				return;
			}
			configuration = FileHandler.GetExcelConfiguration(args[0]);
#endif


			if (configuration == null)
			{
				return;
			}


			ExcelContext? context = FileHandler.GetExcelContext(configuration);
			if (context == null)
			{
				return;
			}

			string output = WorkbookGrouper.ProcessWorkbook(context);
			FileHandler.WriteFile(configuration.PathWithoutExtension + ".txt", output);
		}
	}
}
