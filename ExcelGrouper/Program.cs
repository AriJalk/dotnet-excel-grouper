using ClosedXML.Excel;
using ExcelGrouper.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExcelGrouper
{

	internal class Program
	{

		static void Main(string[] args)
		{
			ExcelConfiguration? configuration = null;
#if DEBUG
			string path = "D:/Users/Ariel/Downloads/Book1.xlsx";
			configuration = new ExcelConfiguration(path, "Sheet1", "A1:C9", ["Test1", "Test2", "Test3"], 2);
#else
			if (string.IsNullOrEmpty(args[0]))
			{
				return;
			}
			configuration = ConfigurationReader.GetConfiguration(args[0]);
#endif


			if (configuration == null || String.IsNullOrEmpty(configuration.WorkbookPath))
			{
				return;
			}

			ExcelHandler handler = new ExcelHandler(configuration);
			handler.ProcessWorkbook();
		}
	}
}
