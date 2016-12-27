using SQLite;

namespace EDDB.Data.Model.Internal
{
	[Table("station_economies")]
	internal class StationEconomy
	{
		[Column("_id")]
		[PrimaryKey]
		[AutoIncrement]
		public int ID { get; set; }

		[Column("station_id")]
		[Indexed("uidx_stationeconomy", 0, Unique = true)]
		public int StationId { get; set; }

		[Column("economy_id")]
		[Indexed("uidx_stationeconomy", 1, Unique = true)]
		public int EconomyId { get; set; }
	}
}
