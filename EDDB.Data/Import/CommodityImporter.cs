using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EDDB.Data.Model;
using Common.Logging;

namespace EDDB.Data.Import
{
	public class CommodityImporter : BaseImporter
	{
		#region Constructor

		public CommodityImporter(ILogger logger, Database db) : base(logger, db) { }

		public CommodityImporter(ILogger logger, Database db, IReportProgress progress) : base(logger, db, progress) { }

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
							Commodity c = serializer.Deserialize<Commodity>(jsonReader);
							_Database.SaveCommodity(c);
							recordsChanged++;
						}
						catch (Exception ex)
						{
							ReportError("Error during Commodity import", ex);
						}
					}
				}
			}
			ReportFinished(recordsChanged);
		}

		#endregion
	}
}
