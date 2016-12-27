using SQLite;

namespace EDDB.Data.Model.Internal
{
	[Table("station_import_commodities")]
	internal class StationImportCommodity
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("_id")]
		public int ID { get; set; }

		[Column("station_id")]
		[Indexed("uidx_stationimportcommodities", 0, Unique = true)]
		public int StationId { get; set; }

		[Column("commodity_id")]
		[Indexed("uidx_stationimportcommodities", 1, Unique = true)]
		public int CommodityId { get; set; }
	}
}
