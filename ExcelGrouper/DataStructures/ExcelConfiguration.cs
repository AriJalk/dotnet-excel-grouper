using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelGrouper
{
	internal class ExcelConfiguration
	{
		public string WorkbookPath { get; set; }
		public string WorksheetName { get; set; }
		public string CellRange { get; set; }
		public IEnumerable<string> Headers { get; set; }
		public float Sensitivity { get; set; }


		public ExcelConfiguration() { }

		public ExcelConfiguration(string workbookPath, string worksheetName, string cellRange, IEnumerable<string> headers, float sensitivity)
		{
			WorkbookPath = workbookPath;
			WorksheetName = worksheetName;
			CellRange = cellRange;
			Headers = headers;
			Sensitivity = Math.Abs(sensitivity);
		}
	}
}
