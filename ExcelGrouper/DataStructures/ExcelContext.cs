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


		public ExcelContext(ExcelConfiguration configuration)
		{
			Workbook = new XLWorkbook(configuration.PathWithoutExtension + ".xlsx");
			Configuration = configuration;
		}
	}
}
