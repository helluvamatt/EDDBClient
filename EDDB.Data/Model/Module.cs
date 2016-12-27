using Newtonsoft.Json;
using SQLite;

namespace EDDB.Data.Model
{
	[Table("modules")]
	[JsonObject(MemberSerialization.OptIn)]
	public class Module
	{
		[JsonProperty("id", Required = Required.Always)]
		[PrimaryKey]
		[Column("_id")]
		public int ID { get; set; }

		[JsonProperty("group_id")]
		[Column("group_id")]
		internal int GroupID { get; set; }

		[JsonProperty("group")]
		[Ignore]
		public ModuleGroup Group { get; set; }

		[Column("ship_id")]
		internal int ShipId { get; set; }

		[Ignore]
		[JsonProperty("ship")]
		internal string ShipName { get; set; }

		[Ignore]
		public Ship Ship { get; set; }

		[JsonProperty("class")]
		[Column("class")]
		public int Class { get; set; }

		[JsonProperty("rating")]
		[Column("rating")]
		public string Rating { get; set; }

		[JsonProperty("price")]
		[Column("price")]
		public int? Price { get; set; }

		[JsonProperty("weapon_mode", Required = Required.AllowNull)]
		[Column("weapon_mode")]
		public string WeaponMode { get; set; }

		[JsonProperty("missile_type", Required = Required.AllowNull)]
		[Column("missile_type")]
		public string MissleType { get; set; }

		[JsonProperty("name", Required = Required.AllowNull)]
		[Column("name")]
		public string Name { get; set; }

		// BelongsTo property omitted

		[JsonProperty("ed_id")]
		[Column("ed_id")]
		public long? EdId { get; set; }

		[JsonProperty("ed_symbol")]
		[Column("ed_symbol")]
		public string EdSymbol { get; set; }
	}
}
