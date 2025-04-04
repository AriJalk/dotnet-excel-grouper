using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelGrouper.DataStructures
{
	internal class MultiDictionary
	{
		private int _groupIndex;
		Dictionary<float, object> MultiDiciontary { get; set; }


		public MultiDictionary()
		{
			MultiDiciontary = new Dictionary<float, object>();
			_groupIndex = 1;
		}

		public int GetGroupId(List<float> values, float sensitivity)
		{
			Dictionary<float, object> currentLevel = MultiDiciontary;
			for (int i = 0; i < values.Count - 1; i++)
			{
				if (GetDictionaryInRange(currentLevel, values[i], sensitivity) is Dictionary<float, object> dict)
				{
					currentLevel = dict;
				}
				else
				{
					currentLevel[values[i]] = new Dictionary<float, object>();
					currentLevel = currentLevel[values[i]] as Dictionary<float, object>;
				}
			}
			float last = values.Last();
			if (GetDictionaryInRange(currentLevel, last, sensitivity) is int groupId && groupId != 0)
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


		private static object? GetDictionaryInRange(Dictionary<float, object> dict, float value, float sensitivity)
		{
			for (int i = 0; i <= sensitivity; i++)
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
