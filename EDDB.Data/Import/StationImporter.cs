using Common.Logging;
using EDDB.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDB.Data.Import
{
	public class StationImporter : BaseImporter
	{
		#region Constructor

		public StationImporter(ILogger logger, Database db) : base(logger, db) { }

		public StationImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

		#endregion

		#region Importer impl

		protected override void DoImport(TextReader reader)
		{
			ReportStarted();
			int recordsChanged = 0;
			using (JsonReader jsonReader = new JsonTextReader(reader))
			{
				JsonSerializer serializer = new JsonSerializer();
				while (jsonReader.Read())
				{
					if (jsonReader.TokenType == JsonToken.StartObject)
					{
						var station = serializer.Deserialize<Station>(jsonReader);
						_Database.SaveStation(station);
					}
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
