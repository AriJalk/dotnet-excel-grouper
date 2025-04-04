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
		private Dictionary<float, object> _multiDiciontary { get; set; }
		private float _sensitivity;
		private int _groupIndex;


		public MultiDictionary(float sensitivity)
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
					currentLevel = dict;
				}
				else
				{
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
