using EDDB.Data.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EDDB.Data.Test
{
	[TestClass]
	public class ImportTests : IReportProgress
	{
		private Database _Database;

		[TestInitialize]
		public void Init()
		{
			_Database = new Database(new DbImpl("EDDB_ImportTest.db3"));
		}

		[TestMethod]
		public void TestImportCommodities()
		{
			var importer = new CommodityImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("commodities.json");
			Assert.IsNotNull(reader, "commodities.json reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestMethod]
		public void TestImportModules()
		{
			var importer = new ModuleImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("modules.json");
			Assert.IsNotNull(reader, "modules.json reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestMethod]
		public void TestImportListings()
		{
			var importer = new ListingImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("listings.csv");
			Assert.IsNotNull(reader, "listings.csv reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestMethod]
		public void TestImportStarSystems()
		{
			var importer = new StarSystemsImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("systems.csv");
			Assert.IsNotNull(reader, "systems.csv reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestMethod]
		public void TestImportPopulatedStarSystems()
		{
			var importer = new PopulatedStarSystemsImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("systems_populated.json");
			Assert.IsNotNull(reader, "systems_populated.json reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestMethod]
		public void TestImportStations()
		{
			var importer = new StationImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("stations.json");
			Assert.IsNotNull(reader, "stations.json reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestMethod]
		public void TestImportBodies()
		{
			var importer = new BodyImporter(_Database, this);
			var reader = Utils.GetResourceTextFile("bodies.json");
			Assert.IsNotNull(reader, "bodies.json reader was null");
			importer.Import(reader);
			Assert.AreEqual(0, importer.Errors.Count, "Import errors");
		}

		[TestCleanup]
		public void Cleanup()
		{
			_Database.Dispose();
		}

		public void OnStarted()
		{
			Console.WriteLine("Import started...");
		}

		public void OnFinished(int recordsChanged)
		{
			Console.WriteLine("Import finished, {0} records changed.", recordsChanged);
		}

		public void OnProgress(float progress)
		{
			Console.WriteLine("Progress: {0:P2}", progress);
		}

		public void OnError(string message)
		{
			Console.Error.WriteLine("Import Error: {0}", message);
		}
	}
}
