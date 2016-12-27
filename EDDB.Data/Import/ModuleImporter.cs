using System;
using System.IO;
using Newtonsoft.Json;
using EDDB.Data.Model;
using Common.Logging;

namespace EDDB.Data.Import
{
	public class ModuleImporter : BaseImporter
	{
		#region Constructor

		public ModuleImporter(ILogger logger, Database db) : base(logger, db) { }

		public ModuleImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

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
						try
						{
							Module module = serializer.Deserialize<Module>(jsonReader);
							_Database.SaveModule(module);
							recordsChanged++;
						}
						catch (Exception ex)
						{
							ReportError("Error during Module import", ex);
						}
					}
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
