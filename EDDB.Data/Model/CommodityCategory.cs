using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDB.Data.Model
{
	[Table("commodity_categories")]
	[JsonObject(MemberSerialization.OptIn)]
	public class CommodityCategory
	{
		[PrimaryKey]
		[Column("_id")]
		[JsonProperty("id", Required = Required.Always)]
		public int ID { get; set; }

		[Column("name")]
		[JsonProperty("name", Required = Required.Always)]
		public string Name { get; set; }

		[Ignore]
		public IEnumerable<Commodity> Commodities { get; set; }
	}
}
