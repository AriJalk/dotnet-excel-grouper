/// Stores List of values grouped by threshold distance of all required values

namespace ExcelGrouper.DataStructures
{
	public class ThresholdGroupedDictionary
	{
		private Dictionary<int, object> _multiDiciontary { get; set; }
		private int _threshold;
		private int _groupIndex;


		public ThresholdGroupedDictionary(int threshold)
		{
			_multiDiciontary = new Dictionary<int, object>();
			_threshold = Math.Abs(threshold);
			_groupIndex = 1;
		}

		public int GetGroupId(List<float> values)
		{
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
			if (GetDictionaryInRange(currentLevel, lastRoundedValue) is int groupId && groupId != 0)
			{
				return groupId;
			}
			else
			{
				currentLevel[lastRoundedValue] = _groupIndex;
				_groupIndex += 1;
				return _groupIndex - 1;
			}
		}


		private object? GetDictionaryInRange(Dictionary<int, object> dict, int value)
		{
			for (int i = 0; i <= _threshold; i++)
			{
				if (dict.ContainsKey(value + i))
				{
					return dict[value + i];
				}
				if (dict.ContainsKey(value - i))
				{
					return dict[value - i];
				}
			}
			return null;
		}
	}
}
