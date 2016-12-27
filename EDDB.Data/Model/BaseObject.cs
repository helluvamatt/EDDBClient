using SQLite;

namespace EDDB.Data.Model
{
	public abstract class BaseObject
	{
		[PrimaryKey]
		[Column("_id")]
		public int ID { get; set; }

		[Column("name")]
		[Unique]
		public string Name { get; set; }
	}
}
