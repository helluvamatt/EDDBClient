using CsvHelper.Configuration;
using EDDB.Data.Converters;
using EDDB.Data.Model;

namespace EDDB.Data.Import.CsvMappings
{
	internal sealed class StarSystemsMapping : CsvClassMap<StarSystem>
	{
		public StarSystemsMapping()
		{
			Map(s => s.ID).Name("id");
			Map(s => s.EdsmId).Name("edsm_id");
			Map(s => s.Name).Name("name");
			Map(s => s.X).Name("x");
			Map(s => s.Y).Name("y");
			Map(s => s.Z).Name("z");
			Map(s => s.Population).Name("population");
			Map(s => s.Populated).Name("is_populated");
			Map(s => s.GovernmentId).Name("government_id");
			Map(s => s.AllegianceId).Name("allegiance_id");
			Map(s => s.StateId).Name("state_id");
			Map(s => s.SecurityId).Name("security_id");
			Map(s => s.PrimaryEconomyId).Name("primary_economy_id");
			// TODO Power?   power,power_state
			Map(s => s.PowerStateId).Name("power_state_id");
			Map(s => s.NeedsPermit).Name("needs_permit").TypeConverter<CsvHelper.TypeConversion.BooleanConverter>();
			Map(s => s.UpdatedAt).Name("updated_at").TypeConverter<UnixTimestampConverter>();
			Map(s => s.ControllingMinorFactionId).Name("controlling_minor_faction_id");
			Map(s => s.ReserveTypeId).Name("reserve_type_id");
		}
	}
}
