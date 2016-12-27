using Common.Logging;
using EDDB.Data.Model;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EDDB.Data.Import
{
	public class BodyImporter : BaseImporter
	{
		#region Constructor

		public BodyImporter(ILogger logger, Database db) : base(logger, db) { }

		public BodyImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

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
						try
						{
							var body = serializer.Deserialize<Body>(jsonReader);
							_Database.SaveBody(body);
							recordsChanged++;
						}
						catch (Exception ex)
						{
							ReportError("Failed to import Body", ex, jsonReader.Value.ToString());
						}
						
					}
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
