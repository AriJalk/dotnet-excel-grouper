/// Handles a workbook from context, passing to relevant parts

using ClosedXML.Excel;
using ExcelGrouper.DataStructures;

namespace ExcelGrouper.Services
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
				switch (configuration.ProcessOption)
				{
					case ExcelConfiguration.ProcessOptions.PARALLEL:
						output = RangeGrouper.GetGroupsFromRangeParallel(worksheet.Range(configuration.CellsRange), configuration.Headers, configuration.Threshold, configuration.Offset);
						break;
					case ExcelConfiguration.ProcessOptions.SYNCHRONOUS:
						output = RangeGrouper.GetGroupsFromRange(worksheet.Range(configuration.CellsRange), configuration.Headers, configuration.Threshold, configuration.Offset);
						break;
				}
			}
			return output;
		}
	}
}
