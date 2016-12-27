using SQLite;

namespace EDDB.Data.Model.Internal
{
	[Table("station_selling_module")]
	internal class StationSellingModule
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("_id")]
		public int ID { get; set; }

		[Column("station_id")]
		[Indexed("uidx_stationsellingmodule", 0, Unique = true)]
		public int StationId { get; set; }

		[Column("module_id")]
		[Indexed("uidx_stationsellingmodule", 1, Unique = true)]
		public int ModuleId { get; set; }

	}
}
