/// Stores List of values grouped by sensitivity distance of all required values

namespace ExcelGrouper.DataStructures
{
	public class SensitivityGroupedDictionary
	{
		private Dictionary<float, object> _multiDiciontary { get; set; }
		private int _sensitivity;
		private int _groupIndex;


		public SensitivityGroupedDictionary(int sensitivity)
		{
			_multiDiciontary = new Dictionary<float, object>();
			_sensitivity = sensitivity;
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
			for (int i = 0; i <= _sensitivity; i++)
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
