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

		public ExcelContext(XLWorkbook workbook)
		{
			Workbook = workbook;
		}

		public ExcelContext(string path)
		{
			Workbook = new XLWorkbook(path);
		}
	}
}
