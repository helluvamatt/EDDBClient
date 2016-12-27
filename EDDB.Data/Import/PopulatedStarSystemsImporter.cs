using Common.Logging;
using EDDB.Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDB.Data.Import
{
	public class PopulatedStarSystemsImporter : BaseImporter
	{
		#region Constructor

		public PopulatedStarSystemsImporter(ILogger logger, Database db) : base(logger, db) { }

		public PopulatedStarSystemsImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

		#endregion

		#region Importer impl

		protected override void DoImport(TextReader reader)
		{
			ReportStarted();
			int recordsChanged = 0;
			using (JsonTextReader jsonReader = new JsonTextReader(reader))
			{
				JsonSerializer serializer = new JsonSerializer();
				while (jsonReader.Read())
				{
					if (jsonReader.TokenType == JsonToken.StartObject)
					{
						using (_Database.LockDatabase())
						{
							JObject obj = JObject.Load(jsonReader);
							int starSystemId = obj.GetValue("id").Value<int>();
							_Database.ClearStarSystemMinorFactionPresences(starSystemId);
							JArray minorFactionPresences = obj["minor_faction_presences"] as JArray;
							if (minorFactionPresences != null)
							{
								foreach (var item in minorFactionPresences.Cast<JObject>())
								{
									var influence = item.GetValue("influence");
									var state = item.GetValue("state_id");
									StarSystemMinorFactionPresence presence = new StarSystemMinorFactionPresence();
									presence.StarSystemId = starSystemId;
									presence.MinorFactionId = item.GetValue("minor_faction_id").Value<int>();
									presence.Influence = influence.Type == JTokenType.Null ? (float?)null : influence.Value<float>();
									presence.StateId = state.Type == JTokenType.Null ? (int?)null : state.Value<int>();
									_Database.SaveStarSystemMinorFactionPresence(presence);
								}
							}
						}
					}
				}
			}

			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
