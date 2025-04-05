/// The nessecary configuration required to create ExcelContext and work with it

namespace ExcelGrouper
{
	internal class ExcelConfiguration
	{
		// Assigned by code
		public string FileName { get; set; } = "";
		// Assigned by code
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
		public int Sensitivity { get; set; }


		public ExcelConfiguration() { }
	}
}
