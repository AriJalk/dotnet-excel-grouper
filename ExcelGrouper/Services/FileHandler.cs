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
			if (Path.Exists(path))
			{
				ExcelConfiguration? configuration = null;
				using (StreamReader streamReader = new StreamReader(path))
				{
					string json = streamReader.ReadToEnd();
					JsonSerializerOptions option = new JsonSerializerOptions()
					{
						PropertyNameCaseInsensitive = true,
					};
					try
					{
						configuration = JsonSerializer.Deserialize<ExcelConfiguration>(json, option);
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.Message);
						return null;
					}
					if (configuration != null)
					{
						configuration.FileName = Path.GetFileNameWithoutExtension(path);
						configuration.Directory = Path.GetDirectoryName(path);
					}
				}
				return configuration;
			}
			throw new FileNotFoundException();
		}

		// Safer method to get context with nullable option if there's a problem
		public static ExcelContext? GetExcelContext(ExcelConfiguration configuration)
		{
			string path = configuration.PathWithoutExtension + ".xlsx";
			if (File.Exists(path) && !IsFileLocked(path))
			{
				return new ExcelContext(configuration);
			}
			return null;
		}

		private static bool IsFileLocked(string path)
		{
			try
			{
				using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					stream.Close();
				}
			}
			catch (IOException er)
			{
				Console.WriteLine(er.Message);
				return true;
			}

			//file is not locked
			return false;
		}
	}
}
