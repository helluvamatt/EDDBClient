using Newtonsoft.Json;
using SQLite;

namespace EDDB.Data.Model
{
	[Table("star_system_minor_faction_presence")]
	[JsonObject(MemberSerialization.OptIn)]
	public class StarSystemMinorFactionPresence
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("_id")]
		public int ID { get; set; }

		[Column("system_id")]
		[Indexed("uidx_starsystemminorfaction", 0, Unique = true)]
		public int StarSystemId { get; set; }

		[Column("minor_faction_id")]
		[Indexed("uidx_starsystemminorfaction", 1, Unique = true)]
		[JsonProperty("minor_faction_id")]
		public int MinorFactionId { get; set; }

		[Ignore]
		public MinorFaction MinorFaction { get; set; }

		[Column("state_id")]
		[JsonProperty("state_id")]
		public int? StateId { get; set; }

		[Ignore]
		public EconomicState State { get; set; }

		[Column("influence")]
		[JsonProperty("influence")]
		public float? Influence { get; set; }
	}
}
