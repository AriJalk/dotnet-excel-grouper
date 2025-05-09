﻿/// Converts range to data usable by the threshold grouper

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using ExcelGrouper.DataStructures;
using System.Collections.Concurrent;
using System.Text;

namespace ExcelGrouper.Services
{
	public class RangeGrouper
	{

		/// <summary>
		/// Get all active columns to be checked
		/// </summary>
		/// <param name="range"></param>
		/// <param name="headers"></param>
		/// <returns>List<int> of the matching columns</returns>
		/// <exception cref="ArgumentException"></exception>
		private static List<int> GetColumns(IXLRange range, IEnumerable<string> headers)
		{
			if (headers.Count() > range.ColumnCount())
			{
				throw new ArgumentException("More headers than columns");
			}
			List<int> columns = new List<int>();
			Queue<string> headerQueue = new Queue<string>(headers);
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

			return columns;
		}

		/// <summary>
		/// Sequential processing of xl range based on headers
		/// </summary>
		/// <param name="range">All rows including headers</param>
		/// <param name="headers">Which header columns to include</param>
		/// <param name="threshold">Max distance from value to be considered close enough</param>
		/// <param name="offset">Group offset</param>
		/// <returns>A string of all groups corelating to rows</returns>
		public static string GetGroupsFromRange(IXLRange range, IEnumerable<string> headers, int threshold, int offset = 0)
		{
			StringBuilder outputBuilder = new StringBuilder();
			ThresholdGroupedDictionary multiDictionary = new ThresholdGroupedDictionary(threshold, offset);
			//DateTime startTime = DateTime.Now;
			for (int row = 2; row <= range.RowCount(); row++)
			{
				List<float> values = new List<float>();
				IXLRangeRow rangeRow = range.Row(row);
				foreach (int column in GetColumns(range, headers))
				{
					values.Add(rangeRow.Cell(column).GetValue<float>());
				}
				outputBuilder.AppendLine(multiDictionary.GetGroupId(values).ToString());
			}


			string output = outputBuilder.ToString();
			//Console.WriteLine($"Sequential: {(DateTime.Now - startTime).TotalMilliseconds}ms");
			return output;

		}

		/// <summary>
		/// Parallel processing of xl range based on headers
		/// </summary>
		/// <param name="range">All rows including headers</param>
		/// <param name="headers">Which header columns to include</param>
		/// <param name="threshold">Max distance from value to be considered close enough</param>
		/// <param name="offset">Group offset</param>
		/// <returns>A string of all groups corelating to rows</returns>
		public static string GetGroupsFromRangeParallel(IXLRange range, IEnumerable<string> headers, int threshold, int offset = 0)
		{
			ThresholdGroupedDictionary multiDictionary = new ThresholdGroupedDictionary(threshold, offset);
			Lock dictLock = new Lock();
			string[] strings = new string[range.RowCount() - 1];
			List<int> columns = GetColumns(range, headers);
			//DateTime startTime = DateTime.Now;

			var partitioner = Partitioner.Create(2, range.RowCount() + 1);


			Parallel.ForEach(partitioner, rowRange =>
			{
				for(int row = rowRange.Item1; row < rowRange.Item2; row++)
				{
					List<float> values = new List<float>();
					IXLRangeRow rangeRow = range.Row(row);
					foreach (int column in columns)
					{
						values.Add(rangeRow.Cell(column).GetValue<float>());
					}
					//Console.WriteLine($"Row {row - 1}");
					string groupString = "";
					lock (dictLock)
					{
						groupString = multiDictionary.GetGroupId(values).ToString();
					}
					strings[row - 2] = groupString;
				}
			});

			StringBuilder outputBuilder = new StringBuilder();
			for (int i = 0; i < strings.Length; i++)
			{
				outputBuilder.Append(strings[i]);
				if (i != strings.Length - 1)
				{
					outputBuilder.Append('\n');
				}
			}

			string output = outputBuilder.ToString();
			//Console.WriteLine($"Parallel: {(DateTime.Now - startTime).TotalMilliseconds}ms");
			return output;
		}

	}
}
