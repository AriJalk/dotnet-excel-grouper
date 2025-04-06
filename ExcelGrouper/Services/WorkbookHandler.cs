/// Handles a workbook from context, passing to relevant parts

using ClosedXML.Excel;
using ExcelGrouper.DataStructures;
using ExcelGrouper.Services;

namespace ExcelGrouper
{
	internal class WorkbookHandler
	{

		public static string ProcessWorkbook(ExcelContext context)
		{
			using XLWorkbook wb = context.Workbook;
			return ProcessSheet(wb, context.Configuration);
		}

		private static string ProcessSheet(IXLWorkbook wb, ExcelConfiguration configuration)
		{
			string output = "";
			wb.TryGetWorksheet(configuration.WorksheetName, out IXLWorksheet worksheet);
			if (worksheet != null)
			{
				output = RangeGrouper.GetGroupsFromRange(worksheet.Range(configuration.CellsRange), configuration.Headers, configuration.Threshold);
			}
			return output;
		}
	}
}
