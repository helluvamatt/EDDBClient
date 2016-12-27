using System;
using System.IO;
using SQLite;

namespace EDDB.Data.Test
{
	internal class DbImpl : IDb
	{
		private string _Filename;

		public DbImpl(string filename)
		{
			_Filename = filename;
		}

		public SQLiteConnection GetConnection()
		{
			var path = Path.Combine(Utils.GetAssemblyDirectory(), _Filename);
			return new SQLiteConnection(path);
		}
	}
}
