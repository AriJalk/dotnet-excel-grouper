/// Stores List of values grouped by threshold distance of all required values

namespace ExcelGrouper.DataStructures
{
	public class ThresholdGroupedDictionary
	{
		private Dictionary<float, object> _multiDiciontary { get; set; }
		private int _threshold;
		private int _groupIndex;


		public ThresholdGroupedDictionary(int threshold)
		{
			_multiDiciontary = new Dictionary<float, object>();
			_threshold = Math.Abs(threshold);
			_groupIndex = 1;
		}

		public int GetGroupId(List<float> values)
		{
			Dictionary<float, object> currentLevel = _multiDiciontary;
			for (int i = 0; i < values.Count - 1; i++)
			{
				if (GetDictionaryInRange(currentLevel, values[i]) is Dictionary<float, object> dict)
				{
					// Progress to matching dictionary
					currentLevel = dict;
				}
				else
				{
					// Initialize new dictionary and advance to it
					currentLevel[values[i]] = new Dictionary<float, object>();
					currentLevel = currentLevel[values[i]] as Dictionary<float, object>;
				}
			}
			float last = values.Last();
			if (GetDictionaryInRange(currentLevel, last) is int groupId && groupId != 0)
			{
				return groupId;
			}
			else
			{
				currentLevel[last] = _groupIndex;
				_groupIndex += 1;
				return _groupIndex - 1;
			}
		}


		private object? GetDictionaryInRange(Dictionary<float, object> dict, float value)
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
