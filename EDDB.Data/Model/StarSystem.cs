using EDDB.Data.Model.Internal;
using SQLite;
using System;
using System.Collections.Generic;

namespace EDDB.Data.Model
{
	[Table("systems")]
	public class StarSystem
	{
		[PrimaryKey]
		[Column("_id")]
		public int ID { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("edsm_id")]
		public int EdsmId { get; set; }

		[Column("x")]
		public float X { get; set; }

		[Column("y")]
		public float Y { get; set; }

		[Column("z")]
		public float Z { get; set; }

		[Column("population")]
		public long Population { get; set; }

		[Column("government_id")]
		internal int GovernmentId { get; set; }

		[Ignore]
		public Government Government { get; set; }

		[Column("allegiance_id")]
		internal int AllegianceId { get; set; }

		[Ignore]
		public Superpower Allegiance { get; set; }

		[Column("state_id")]
		internal int StateId { get; set; }

		[Ignore]
		public EconomicState State { get; set; }

		[Column("security_id")]
		internal int SecurityId { get; set; }

		[Ignore]
		public Security Security { get; set; }

		[Column("primary_economy_id")]
		internal int PrimaryEconomyId { get; set; }

		[Ignore]
		public Economy PrimaryEconomy { get; set; }

		[Column("controlling_minor_faction_id")]
		internal int ControllingMinorFactionId { get; set; }

		[Ignore]
		public MinorFaction ControllingMinorFaction { get; set; }

		[Column("reserve_type_id")]
		internal int ReserveTypeId { get; set; }

		[Ignore]
		public ReserveType ReserveType { get; set; }

		[Ignore]
		internal List<StarSystemMinorFactionPresence> MinorFactionPresenceIds { get; set; }

		[Ignore]
		public ISet<StarSystemMinorFactionPresence> MinorFactionPresences { get; set; }

		[Column("power")]
		public string Power { get; set; }

		[Column("power_state_id")]
		public int? PowerStateId { get; set; }

		[Column("needs_permit")]
		public bool NeedsPermit { get; set; }

		[Column("populated")]
		public bool Populated { get; set; }

		[Column("updated_at")]
		public DateTime UpdatedAt { get; set; }

	}
}
