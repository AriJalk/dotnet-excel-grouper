/// Stores List of values grouped by threshold distance of all required values

namespace ExcelGrouper.DataStructures
{
	public class ThresholdGroupedDictionary
	{
		private Dictionary<int, object> _multiDiciontary;

		private int _threshold;
		private int _offset;

		private int _groupIndex;

		private string _currentDict;

		public ThresholdGroupedDictionary(int threshold, int offset)
		{
			_multiDiciontary = new Dictionary<int, object>();
			_threshold = Math.Abs(threshold);
			_groupIndex = 1;
			_offset = offset;

		}

		public int GetGroupId(List<float> values)
		{
			//_currentDict = "";
			Dictionary<int, object> currentLevel = _multiDiciontary;
			for (int i = 0; i < values.Count - 1; i++)
			{
				int roundedValue = (int)MathF.Round(values[i]);
				if (GetDictionaryInRange(currentLevel, roundedValue) is Dictionary<int, object> dict)
				{
					// Progress to matching dictionary
					currentLevel = dict;
				}
				else
				{
					// Initialize new dictionary and advance to it
					currentLevel[roundedValue] = new Dictionary<int, object>();
					currentLevel = currentLevel[roundedValue] as Dictionary<int, object>;
				}
			}
			int lastRoundedValue = (int)MathF.Round(values.Last());
			// Matching dictionary
			if (GetDictionaryInRange(currentLevel, lastRoundedValue) is int groupId && groupId != 0)
			{
				//Console.WriteLine($"Values: {string.Join(';', values)}, dict: {_currentDict}");
				return groupId;
			}
			// Unique dictionary
			else
			{
				groupId = _groupIndex + _offset;
				currentLevel[lastRoundedValue] = groupId;
				_groupIndex += 1;
				//Console.WriteLine($"Values: {string.Join(';', values)}, dict: {_currentDict + lastRoundedValue.ToString()}");
				return groupId;
			}
		}


		private object? GetDictionaryInRange(Dictionary<int, object> dict, int value)
		{
			for (int i = 0; i <= _threshold; i++)
			{
				if (dict.ContainsKey(value + i))
				{
					//_currentDict += (value + i).ToString() + ";";
					return dict[value + i];
				}
				if (dict.ContainsKey(value - i))
				{
					//_currentDict += (value - i).ToString() + ";";
					return dict[value - i];
				}
			}
			return null;
		}
	}
}
