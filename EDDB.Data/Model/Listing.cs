using SQLite;
using System;

namespace EDDB.Data.Model
{
	[Table("listings")]
	public class Listing
	{
		[PrimaryKey]
		[Column("_id")]
		public int ID { get; set; }

		[Column("station_id")]
		public int StationID { get; set; }

		[Ignore]
		public Station Station { get; set; }

		[Column("commodity_id")]
		public int CommodityID { get; set; }

		[Ignore]
		public Commodity Commodity { get; set; }

		[Column("supply")]
		public int Supply { get; set; }

		[Column("demand")]
		public int Demand { get; set; }

		[Column("buy_price")]
		public int BuyPrice { get; set; }

		[Column("sell_price")]
		public int SellPrice { get; set; }

		[Column("collected_at")]
		public DateTime CollectedAt { get; set; }
	}
}
