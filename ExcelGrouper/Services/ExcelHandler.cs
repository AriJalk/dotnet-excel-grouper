using ClosedXML.Excel;
using ExcelGrouper.DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelGrouper
{
	internal class ExcelHandler
	{
		private ExcelConfiguration _configuration;

		private List<int> _columns;

		private MultiDictionary _multiDictionary;

		private string _output = "";

		public ExcelHandler(ExcelConfiguration configuration)
		{
			_configuration = configuration;
			_columns = new List<int>();
			_multiDictionary = new MultiDictionary();
		}

		public void ProcessWorkbook()
		{
			try
			{
				Console.WriteLine("Loading file");
				using XLWorkbook wb = new XLWorkbook(_configuration.WorkbookPath);
				ProcessSheet(wb);
				string newFileName = Path.GetFileNameWithoutExtension(_configuration.WorkbookPath) + ".txt";
				string newPath = Path.Combine(Path.GetDirectoryName(_configuration.WorkbookPath), newFileName);
				using (StreamWriter sw = new StreamWriter(newPath, false))
				{
					sw.Write(_output);
					Console.WriteLine($"{newPath} created successfuly");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		private void ProcessSheet(IXLWorkbook wb)
		{
			wb.TryGetWorksheet(_configuration.WorksheetName, out IXLWorksheet worksheet);
			if (worksheet != null)
			{
				Queue<string> headersQueue = new Queue<string>(_configuration.Headers);
				IXLRange range = worksheet.Range(_configuration.CellRange);
				IXLRangeRow headerRow = range.Row(1);
				// Find header columns
				for (int col = 1; col <= range.ColumnCount(); col++)
				{
					if (String.Equals(headerRow.Cell(col).Value.ToString(), headersQueue.Peek()))
					{
						_columns.Add(col);
						headersQueue.Dequeue();
						if (headersQueue.Count == 0)
						{
							break;
						}
					}
				}
				for (int row = 2; row <= range.RowCount(); row++)
				{
					List<float> values = new List<float>();
					IXLRangeRow rangeRow = range.Row(row);
					foreach (int column in _columns)
					{
						values.Add(float.Parse(rangeRow.Cell(column).Value.ToString()));
					}
					Console.WriteLine($"Row {row - 1}");
					_output += _multiDictionary.GetGroupId(values, _configuration.Sensitivity) + "\n";
				}
			}
		}
	}
}
