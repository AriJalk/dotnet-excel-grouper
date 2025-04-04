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
		public string FileName { get; set; } = "";
		public string Directory { get; set; } = "";
		public string PathWithoutExtension
		{
			get
			{
				return Path.Combine(Directory, FileName);
			}
		}
		public string WorksheetName { get; set; }
		public string CellsRange { get; set; }
		public IEnumerable<string> Headers { get; set; }
		public float Sensitivity { get; set; }


		public ExcelConfiguration() { }
	}
}
