/// Converts range to data usable by the sensitivity grouper

using ClosedXML.Excel;
using ExcelGrouper.DataStructures;

namespace ExcelGrouper.Services
{
	public class RangeGrouper
	{

		public static string GetGroupsFromRange(IXLRange range, IEnumerable<string> headers, int sensitivity)
		{
			if (headers.Count() > range.ColumnCount())
			{
				return "More headers than columns";
			}
			List<int> columns = new List<int>();
			string output = "";
			Queue<string> headerQueue = new Queue<string>(headers);
			SensitivityGroupedDictionary multiDictionary = new SensitivityGroupedDictionary(sensitivity);
			IXLRangeRow headerRow = range.Row(1);
			// Find header columns
			for (int col = 1; col <= range.ColumnCount(); col++)
			{
				if (String.Equals(headerRow.Cell(col).GetValue<string>(), headerQueue.Peek()))
				{
					columns.Add(col);
					headerQueue.Dequeue();
					if (headerQueue.Count == 0)
					{
						break;
					}
				}
			}
			for (int row = 2; row <= range.RowCount(); row++)
			{
				List<float> values = new List<float>();
				IXLRangeRow rangeRow = range.Row(row);
				foreach (int column in columns)
				{
					values.Add(rangeRow.Cell(column).GetValue<float>());
				}
				Console.WriteLine($"Row {row - 1}");
				output += multiDictionary.GetGroupId(values) + "\n";
			}
			return output;
		}

	}
}
