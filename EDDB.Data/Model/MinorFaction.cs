using SQLite;
using System;
using Newtonsoft.Json;
using EDDB.Data.Converters;

namespace EDDB.Data.Model
{
	[Table("minor_factions")]
	public class MinorFaction
	{
		[PrimaryKey]
		[Column("_id")]
		[JsonProperty("id", Required = Required.Always)]
		public int ID { get; set; }

		[Column("name")]
		[JsonProperty("name")]
		public string Name { get; set; }

		[Column("government_id")]
		[JsonProperty("government_id")]
		public int? GovernmentId { get; set; }

		[Ignore]
		public Government Government { get; set; }

		[JsonProperty("allegiance_id")]
		[Column("allegiance_id")]
		public int? AllegianceId { get; set; }

		[Ignore]
		public Superpower Allegiance { get; set; }

		[JsonProperty("state_id")]
		[Column("state_id")]
		public int? StateId { get; set; }

		[Ignore]
		public EconomicState State { get; set; }

		[JsonProperty("home_system_id", Required = Required.Always)]
		[Column("home_system_id")]
		public int HomeSystemId { get; set; }

		[Ignore]
		public StarSystem HomeSystem { get; set; }

		[JsonProperty("is_player_faction", ItemConverterType = typeof(BoolConverter))]
		[Column("is_player_faction")]
		public bool IsPlayerFaction { get; set; }

		[JsonProperty("updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		[Column("updated_at")]
		public DateTime UpdatedAt { get; set; }
	}
}
