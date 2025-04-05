using ClosedXML.Excel;
using ExcelGrouper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	public class RangeGrouperTests
	{
		IXLWorkbook wb;
		IXLWorksheet ws;

		public RangeGrouperTests()
		{
			wb = new XLWorkbook();
			ws = wb.AddWorksheet("Sheet1");
		}

		[Fact]
		public void ValuesInRange_ReturnsSameGroup()
		{
			int sensitivity = 2;
			// Arrange
			object[,] data = new object[,]
			{
				{ "Test1", "Test2", "Test3", "Test4" },
				{ 1, 1, 1, 1 },
				{ 1, 1, 1, 3 },
				{ 1, 1, 2, 3 },
				{ -1, -1, 2, 3 },
			};

			IXLRange range = ws.Range("A1:D5");
			range.Cell("A1").Value = "Test";
			AssignTable(data, range);

			// Act
			string expected = "1\n1\n1\n1\n";
			string actual = RangeGrouper.GetGroupsFromRange(range, ["Test1", "Test2", "Test3", "Test4"], sensitivity);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WhenValuesAreNotInRange_ItReturnsDifferentGroups()
		{
			int sensitivity = 0;
			// Arrange
			object[,] data = new object[,]
			{
				{ "Test1", "Test2", "Test3", "Test4" },
				{ 1, 1, 1, 1 },
				{ 1, 1, 1, 3 },
				{ 1, 1, 2, 3 },
				{ -1, -1, 2, 3 },
			};

			IXLRange range = ws.Range("A1:D5");
			range.Cell("A1").Value = "Test";
			AssignTable(data, range);

			// Act
			string expected = "1\n2\n3\n4\n";
			string actual = RangeGrouper.GetGroupsFromRange(range, ["Test1", "Test2", "Test3", "Test4"], sensitivity);

			// Assert
			Assert.Equal(expected, actual);
		}

		private void AssignTable(object[,] table, IXLRange range)
		{
			for (int i = 0; i < table.GetLength(1); i++)
			{
				range.Cell(1, i + 1).Value = table[0, i].ToString();
			}
			for (int i = 1; i < table.GetLength(0); i++)
			{
				for (int j = 0; j < table.GetLength(1); j++)
				{
					range.Cell(i + 1, j + 1).Value = float.Parse(table[i, j].ToString());
				}
			}
		}
	}
}
