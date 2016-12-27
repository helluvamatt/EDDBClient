using SQLite;

namespace EDDB.Data.Model.Internal
{
	[Table("station_banned_commodities")]
	internal class StationBannedCommodity
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("_id")]
		public int ID { get; set; }

		[Column("station_id")]
		[Indexed("uidx_stationbannedcommodities_station_id", 0, Unique = true)]
		public int StationId { get; set; }

		[Column("commodity_id")]
		[Indexed("uidx_stationbannedcommodities_station_id", 1, Unique = true)]
		public int CommodityId { get; set; }
	}
}
