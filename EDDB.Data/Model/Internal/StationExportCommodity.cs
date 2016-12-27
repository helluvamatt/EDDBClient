using SQLite;

namespace EDDB.Data.Model.Internal
{
	[Table("station_export_commodities")]
	internal class StationExportCommodity
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("_id")]
		public int ID { get; set; }

		[Column("station_id")]
		[Indexed("uidx_stationexportcommodities", 0, Unique = true)]
		public int StationId { get; set; }

		[Column("commodity_id")]
		[Indexed("uidx_stationexportcommodities", 0, Unique = true)]
		public int CommodityId { get; set; }
	}
}
