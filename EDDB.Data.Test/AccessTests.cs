using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EDDB.Data.Test
{
	[TestClass]
	public class AccessTests
	{
		private Database _Database;

		[TestInitialize]
		public void Init()
		{
			var filename = "EDDB_AccessTests.db3";
			Utils.ExtractFile(filename, Path.Combine(Utils.GetAssemblyDirectory(), filename));
			_Database = new Database(new DbImpl(filename));
		}

		[TestMethod]
		public void TestGetOne()
		{

		}

		[TestMethod]
		public void TestGetAll()
		{


		}

		[TestCleanup]
		public void Cleanup()
		{
			_Database.Dispose();
		}
	}
}
