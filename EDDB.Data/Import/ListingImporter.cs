using Common.Logging;
using CsvHelper;
using EDDB.Data.Import.CsvMappings;
using EDDB.Data.Model;
using System;
using System.IO;

namespace EDDB.Data.Import
{
	public class ListingImporter : BaseImporter
	{
		#region Constructor

		public ListingImporter(ILogger logger, Database db) : base(logger, db) { }

		public ListingImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

		#endregion

		#region Importer impl

		protected override void DoImport(TextReader reader)
		{
			ReportStarted();
			int recordsChanged = 0;
			var csv = new CsvReader(reader);
			csv.Configuration.RegisterClassMap<ListingMapping>();
			while (csv.Read())
			{
				try
				{
					var listing = csv.GetRecord<Listing>();
					if (listing != null)
					{
						_Database.SaveListing(listing);
						recordsChanged++;
					}
				}
				catch (Exception ex)
				{
					ReportError("Error during Listing import", ex);
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
