using ClosedXML.Excel;
using ExcelGrouper.DataStructures;
using ExcelGrouper.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelGrouper
{
	internal class WorkbookGrouper
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
				output = RangeGrouper.GetGroupsFromRange(worksheet.Range(configuration.CellRange), configuration.Headers, configuration.Sensitivity);
			}
			return output;
		}
	}
}
