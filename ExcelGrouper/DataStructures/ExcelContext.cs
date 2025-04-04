using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelGrouper.DataStructures
{
	internal class ExcelContext
	{
		public XLWorkbook Workbook { get; }
		public ExcelConfiguration Configuration { get; }
		
		public ExcelContext(XLWorkbook workbook, ExcelConfiguration configuration)
		{
			Workbook = workbook;
			Configuration = configuration;
		}

		// Load context from file specified in configuration
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
