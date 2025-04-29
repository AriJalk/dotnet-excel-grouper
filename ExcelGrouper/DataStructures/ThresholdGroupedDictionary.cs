
namespace ExcelGrouper.DataStructures
{
	/// <summary>
	/// Stores List of values grouped by threshold distance of all required values
	/// </summary>
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

		/// <summary>
		/// Assigns a group id based on previous encounters, based on threshold proximity.
		/// If no group is found a new group is created based on the encountered current values
		/// </summary>
		/// <param name="values">The row values to be checked</param>
		/// <returns>An integer with the group number (group numbers are sequential)</returns>
		public int GetGroupId(List<float> values)
		{
			//_currentDict = "";
			Dictionary<int, object> currentLevel = _multiDiciontary;
			for (int i = 0; i < values.Count - 1; i++)
			{
				int roundedValue = (int)MathF.Round(values[i]);
				// Check if the dictionary contains a group close to the value checked
				if (GetDictionaryInRange(currentLevel, roundedValue) is Dictionary<int, object> dict)
				{
					// Progress to matching dictionary
					currentLevel = dict;
				}
				// No dictionary found, Initialize new dictionary based on current value and advance to it
				else
				{
					
					currentLevel[roundedValue] = new Dictionary<int, object>();
					currentLevel = currentLevel[roundedValue] as Dictionary<int, object>;
				}
			}
			// Check last value
			int lastRoundedValue = (int)MathF.Round(values.Last());
			// Matching final dictionary in the chain
			if (GetDictionaryInRange(currentLevel, lastRoundedValue) is int groupId && groupId != 0)
			{
				//Console.WriteLine($"Values: {string.Join(';', values)}, dict: {_currentDict}");
				return groupId;
			}
			// Unique, create new group based on current values
			else
			{
				groupId = _groupIndex + _offset;
				currentLevel[lastRoundedValue] = groupId;
				_groupIndex += 1;
				//Console.WriteLine($"Values: {string.Join(';', values)}, dict: {_currentDict + lastRoundedValue.ToString()}");
				return groupId;
			}
		}

		/// <summary>
		/// Checks current value against existing groups within threshold and supplies matching dictionary.
		/// </summary>
		/// <param name="dict"></param>
		/// <param name="value"></param>
		/// <returns>A dictionary of either dictionary values, or a dictionary of integer values</returns>
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
