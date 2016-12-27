using SQLite;
using Newtonsoft.Json;
using EDDB.Data.Converters;

namespace EDDB.Data.Model
{
	[Table("commodities")]
	[JsonObject(MemberSerialization.OptIn)]
	public class Commodity
	{
		[PrimaryKey]
		[Column("_id")]
		[JsonProperty("id", Required = Required.Always)]
		public int ID { get; set; }

		[Column("category_id")]
		[JsonProperty("category_id", Required = Required.Always)]
		internal int CategoryId { get; set; }

		[Column("name")]
		[Unique]
		[JsonProperty("name", Required = Required.Always)]
		public string Name { get; set; }

		[Column("is_rare")]
		[JsonProperty("is_rare", Required = Required.Always, ItemConverterType = typeof(BoolConverter))]
		public bool IsRare { get; set; }

		[Column("average_price")]
		[JsonProperty("average_price", Required = Required.Always)]
		public int AveragePrice { get; set; }

		[Ignore]
		[JsonProperty("category", Required = Required.Always)]
		public CommodityCategory Category { get; set; }

		internal Commodity BuildWithCategory(CommodityCategory cat)
		{
			Category = cat;
			return this;
		}
	}
}
