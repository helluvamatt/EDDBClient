using Common.Logging;
using CsvHelper;
using EDDB.Data.Import.CsvMappings;
using EDDB.Data.Model;
using System;
using System.IO;

namespace EDDB.Data.Import
{
	public class MinorFactionImporter : BaseImporter
	{
		#region Constructor

		public MinorFactionImporter(ILogger logger, Database db) : base(logger, db) { }

		public MinorFactionImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

		#endregion

		#region Importer impl

		protected override void DoImport(TextReader reader)
		{
			ReportStarted();
			int recordsChanged = 0;
			var csv = new CsvReader(reader);
			csv.Configuration.RegisterClassMap<MinorFactionMapping>();
			while (csv.Read())
			{
				try
				{
					var minorFaction = csv.GetRecord<MinorFaction>();
					if (minorFaction != null)
					{
						_Database.SaveMinorFaction(minorFaction);
						recordsChanged++;
					}
				}
				catch (Exception ex)
				{
					ReportError("Error during MinorFaction import", ex);
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
