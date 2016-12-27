using System;
using System.IO;
using System.Reflection;

namespace EDDB.Data.Test
{
	public static class Utils
	{
		public static string GetAssemblyDirectory()
		{
			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			return Path.GetDirectoryName(path);
		}

		public static TextReader GetEmbeddedTextFile(string resourceName)
		{
			return new StreamReader(GetEmbeddedResourceStream(resourceName));
		}

		public static void ExtractFile(string resourceName, string toPath)
		{
			using (Stream resource = GetEmbeddedResourceStream(resourceName))
			{
				using (FileStream fs = new FileStream(toPath, FileMode.Create))
				{
					resource.CopyTo(fs);
				}
			}
		}

		public static Stream GetEmbeddedResourceStream(string resourceName)
		{
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			if (stream == null) throw new IOException("Resource not found: " + resourceName);
			return stream;
		}

		public static TextReader GetResourceTextFile(string resourceName)
		{
			var path = Path.Combine(GetAssemblyDirectory(), "Resources", resourceName);
			return new StreamReader(path);
		}
	}
}
