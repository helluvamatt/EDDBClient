using SQLite;

namespace EDDB.Data.Model
{
	[Table("module_group_categories")]
	public class ModuleGroupCategory
	{
		[PrimaryKey]
		[Column("_id")]
		public int ID { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}
