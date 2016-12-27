using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;
using EDDB.Data.Converters;

namespace EDDB.Data.Model
{
	[Table("stations")]
	[JsonObject(MemberSerialization.OptIn)]
	public class Station
	{
		[PrimaryKey]
		[Column("_id")]
		[JsonProperty("id")]
		public int ID { get; set; }

		[Column("name")]
		[JsonProperty("name")]
		public string Name { get; set; }

		[Column("system_id")]
		[JsonProperty("system_id")]
		internal int SystemID { get; set; }

		[Ignore]
		public StarSystem System { get; set; }

		[Column("max_landing_pad_size")]
		[JsonProperty("max_landing_pad_size")]
		[MaxLength(1)]
		public string MaxLandingPadSize { get; set; }

		[Column("distance_to_star")]
		[JsonProperty("distance_to_star")]
		public int DistanceToStar { get; set; }

		[Column("government_id")]
		[JsonProperty("government_id")]
		internal int GovernmentId { get; set; }

		[Ignore]
		public Government Government { get; set; }

		[Column("allegiance_id")]
		[JsonProperty("allegiance_id")]
		internal int AllegianceId { get; set; }

		[Ignore]
		public Superpower Allegiance { get; set; }

		[Column("state_id")]
		[JsonProperty("state_id")]
		internal int StateId { get; set; }

		[Ignore]
		public EconomicState State { get; set; }

		[Column("type_id")]
		[JsonProperty("type_id")]
		internal int TypeId { get; set; }

		[Ignore]
		public StationType Type { get; set; }

		[Column("controlling_minor_faction_id")]
		[JsonProperty("controlling_minor_faction_id")]
		internal int ControllingMinorFactionId { get; set; }

		[Ignore]
		public MinorFaction ControllingMinorFaction { get; set; }

		[Column("settlement_size_id")]
		[JsonProperty("settlement_size_id")]
		public int? SettlementSizeId { get; set; }

		[Ignore]
		public SettlementSize SettlementSize { get; set; }

		[Column("settlement_security_id")]
		[JsonProperty("settlement_security_id")]
		internal int? SettlementSecurityId { get; set; }

		[Ignore]
		public Security SettlementSecurity { get; set; }

		[Column("body_id")]
		[JsonProperty("body_id")]
		internal int? BodyId { get; set; }

		[Ignore]
		public Body Body { get; set; }

		[Column("has_blackmarket")]
		[JsonProperty("has_blackmarket")]
		public bool HasBlackmarket { get; set; }

		[Column("has_market")]
		[JsonProperty("has_market")]
		public bool HasMarket { get; set; }

		[Column("has_refuel")]
		[JsonProperty("has_refuel")]
		public bool HasRefuel { get; set; }

		[Column("has_repair")]
		[JsonProperty("has_repair")]
		public bool HasRepair { get; set; }

		[Column("has_rearm")]
		[JsonProperty("has_rearm")]
		public bool HasRearm { get; set; }

		[Column("has_outfitting")]
		[JsonProperty("has_outfitting")]
		public bool HasOutfitting { get; set; }

		[Column("has_shipyard")]
		[JsonProperty("has_shipyard")]
		public bool HasShipyard { get; set; }

		[Column("has_docking")]
		[JsonProperty("has_docking")]
		public bool HasDocking { get; set; }

		[Column("has_commodities")]
		[JsonProperty("has_commodities")]
		public bool HasCommodities { get; set; }

		[Column("is_planetary")]
		[JsonProperty("is_planetary")]
		public bool IsPlanetary { get; set; }

		[Ignore]
		[JsonProperty("import_commodities")]
		internal List<string> ImportCommodityNames { get; set; }

		[Ignore]
		public ISet<Commodity> ImportCommodities { get; set; }

		[Ignore]
		[JsonProperty("export_commodities")]
		internal List<string> ExportCommodityNames { get; set; }

		[Ignore]
		public ISet<Commodity> ExportCommodities { get; set; }

		[Ignore]
		[JsonProperty("banned_commodities")]
		internal List<string> BannedCommodityNames { get; set; }

		[Ignore]
		public ISet<Commodity> BannedCommodities { get; set; }

		[Ignore]
		[JsonProperty("economies")]
		internal List<string> EconomyNames { get; set; }

		[Ignore]
		public ISet<Economy> Economies { get; set; }

		[Ignore]
		[JsonProperty("selling_ships")]
		internal List<string> SellingShipNames { get; set; }

		[Ignore]
		public ISet<Ship> SellingShips { get; set; }

		[Ignore]
		[JsonProperty("selling_modules")]
		internal List<int> SellingModuleIds { get; set; }

		[Ignore]
		public ISet<Module> SellingModules { get; set; }

		[Column("shipyard_updated_at")]
		[JsonProperty("shipyard_updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime? ShipyardUpdateAt { get; set; }

		[Column("outfitting_updated_at")]
		[JsonProperty("outfitting_updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime? OutfittingUpdatedAt { get; set; }

		[Column("market_updated_at")]
		[JsonProperty("market_updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime? MarketUpdatedAt { get; set; }

		[Column("updated_at")]
		[JsonProperty("updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime UpdatedAt { get; set; }
	}
}
