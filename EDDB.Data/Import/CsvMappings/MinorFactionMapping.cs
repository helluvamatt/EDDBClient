using CsvHelper.Configuration;
using EDDB.Data.Converters;
using EDDB.Data.Model;

namespace EDDB.Data.Import.CsvMappings
{
	internal sealed class MinorFactionMapping : CsvClassMap<MinorFaction>
	{
		public MinorFactionMapping()
		{
			Map(f => f.ID).Name("id");
			Map(f => f.Name).Name("name");
			Map(f => f.UpdatedAt).Name("updated_at").TypeConverter<UnixTimestampConverter>();
			Map(f => f.GovernmentId).Name("government_id");
			Map(f => f.AllegianceId).Name("allegiance_id");
			Map(f => f.StateId).Name("state_id");
			Map(f => f.HomeSystemId).Name("home_system_id");
			Map(f => f.IsPlayerFaction).Name("is_player_faction").TypeConverter<CsvHelper.TypeConversion.BooleanConverter>();
		}
	}
}
