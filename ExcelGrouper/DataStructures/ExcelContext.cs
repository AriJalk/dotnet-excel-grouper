/// Access layer for a workbook

using ClosedXML.Excel;

namespace ExcelGrouper.DataStructures
{
	internal class ExcelContext
	{
		public XLWorkbook Workbook { get; }
		public ExcelConfiguration Configuration { get; }
		
		/// <summary>
		/// Create context with already loaded workbook
		/// </summary>
		/// <param name="workbook"></param>
		/// <param name="configuration"></param>
		public ExcelContext(XLWorkbook workbook, ExcelConfiguration configuration)
		{
			Workbook = workbook;
			Configuration = configuration;
		}

		/// <summary>
		///  Load context from file specified in configuration
		/// </summary>
		/// <param name="configuration"></param>
		public ExcelContext(ExcelConfiguration configuration)
		{
			string path = configuration.PathWithoutExtension + ".xlsx";
			Console.WriteLine($"Opening file: {path}");
			Workbook = new XLWorkbook(path);
			Configuration = configuration;
			Console.WriteLine($"File opened");
		}
	}
}
