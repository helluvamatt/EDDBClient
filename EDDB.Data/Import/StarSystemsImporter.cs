using Common.Logging;
using CsvHelper;
using EDDB.Data.Import.CsvMappings;
using EDDB.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDB.Data.Import
{
	public class StarSystemsImporter : BaseImporter
	{
		#region Constructor

		public StarSystemsImporter(ILogger logger, Database db) : base(logger, db) { }

		public StarSystemsImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

		#endregion

		#region Importer impl

		protected override void DoImport(TextReader reader)
		{
			ReportStarted();
			int recordsChanged = 0;
			var csv = new CsvReader(reader);
			csv.Configuration.RegisterClassMap<StarSystemsMapping>();
			while (csv.Read())
			{
				try
				{
					var starSystem = csv.GetRecord<StarSystem>();
					if (starSystem != null)
					{
						string governmentName = csv.GetField("government");
						if (!string.IsNullOrWhiteSpace(governmentName))
						{
							Government government = new Government();
							government.ID = starSystem.GovernmentId;
							government.Name = governmentName;
							starSystem.Government = government;
						}

						string allegianceName = csv.GetField("allegiance");
						if (!string.IsNullOrWhiteSpace(allegianceName))
						{
							Superpower allegiance = new Superpower();
							allegiance.ID = starSystem.AllegianceId;
							allegiance.Name = allegianceName;
							starSystem.Allegiance = allegiance;
						}

						string stateName = csv.GetField("state");
						if (!string.IsNullOrWhiteSpace(stateName))
						{
							EconomicState state = new EconomicState();
							state.ID = starSystem.StateId;
							state.Name = stateName;
							starSystem.State = state;
						}

						string securityName = csv.GetField("security");
						if (!string.IsNullOrWhiteSpace(securityName))
						{
							Security security = new Security();
							security.ID = starSystem.SecurityId;
							security.Name = securityName;
							starSystem.Security = security;
						}

						string primaryEconomyName = csv.GetField("primary_economy");
						if (!string.IsNullOrWhiteSpace(primaryEconomyName))
						{
							Economy primaryEconomy = new Economy();
							primaryEconomy.ID = starSystem.PrimaryEconomyId;
							primaryEconomy.Name = primaryEconomyName;
							starSystem.PrimaryEconomy = primaryEconomy;
						}

						string reserveTypeName = csv.GetField("reserve_type");
						if (!string.IsNullOrWhiteSpace(reserveTypeName))
						{
							ReserveType reserveType = new ReserveType();
							reserveType.ID = starSystem.ReserveTypeId;
							reserveType.Name = reserveTypeName;
							starSystem.ReserveType = reserveType;
						}

						_Database.SaveStarSystem(starSystem);

						recordsChanged++;
					}
				}
				catch (Exception ex)
				{
					ReportError("Error during StarSystem import", ex);
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
