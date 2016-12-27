using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDB.Data.Model.Internal
{
	[Table("station_selling_ship")]
	internal class StationSellingShip
	{
		[Column("_id")]
		[PrimaryKey]
		[AutoIncrement]
		public int ID { get; set; }

		[Column("station_id")]
		[Indexed("uidx_stationsellingship", 0, Unique = true)]
		public int StationId { get; set; }

		[Column("ship_id")]
		[Indexed("uidx_stationsellingship", 1, Unique = true)]
		public int ShipId { get; set; }
	}
}
