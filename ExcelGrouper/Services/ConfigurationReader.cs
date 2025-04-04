using ExcelGrouper.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExcelGrouper.Services
{
	internal class ConfigurationReader
	{
		public static ExcelConfiguration? GetConfiguration(string path)
		{
			using (StreamReader streamReader = new StreamReader(path))
			{
				string json = streamReader.ReadToEnd();
				JsonSerializerOptions option = new JsonSerializerOptions()
				{
					PropertyNameCaseInsensitive = true,
				};

				return JsonSerializer.Deserialize<ExcelConfiguration>(json, option);
			}
		}
	}
}
