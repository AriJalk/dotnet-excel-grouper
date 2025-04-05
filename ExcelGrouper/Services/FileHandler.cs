/// Handles file loading/writing

using ExcelGrouper.DataStructures;
using System.Text.Json;


namespace ExcelGrouper.Services
{
	internal class FileHandler
	{
		public static void WriteFile(string path, string content)
		{
			using (StreamWriter sw = new StreamWriter(path, false))
			{
				sw.Write(content);
				Console.WriteLine($"{path} created successfuly");
			}
		}

		// Gets configuration from JsonFile
		public static ExcelConfiguration? GetExcelConfiguration(string path)
		{
			ExcelConfiguration? configuration = null;
			if (Path.Exists(path))
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					string json = streamReader.ReadToEnd();
					JsonSerializerOptions option = new JsonSerializerOptions()
					{
						PropertyNameCaseInsensitive = true,
					};
					configuration = JsonSerializer.Deserialize<ExcelConfiguration>(json, option);
					if (configuration != null)
					{
						configuration.FileName = Path.GetFileNameWithoutExtension(path);
						configuration.Directory = Path.GetDirectoryName(path);
					}
				}
			}
			return configuration;

		}

		// Safer method to get context with nullable option if there's a problem
		public static ExcelContext? GetExcelContext(ExcelConfiguration configuration)
		{
			string path = configuration.PathWithoutExtension + ".xlsx";
			if (File.Exists(path))
			{
				return new ExcelContext(configuration);
			}
			return null;
		}
	}
}
