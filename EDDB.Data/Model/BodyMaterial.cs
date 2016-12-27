using Newtonsoft.Json;
using SQLite;

namespace EDDB.Data.Model
{
	[Table("body_materials")]
	[JsonObject(MemberSerialization.OptIn)]
	public class BodyMaterial
	{
		[Column("_id")]
		[PrimaryKey]
		[AutoIncrement]
		public int ID { get; set; }

		[Column("body_id")]
		[Indexed("uidx_bodymaterial", 0, Unique = true)]
		internal int BodyId { get; set; }

		[Column("material_id")]
		[Indexed("uidx_bodymaterial", 1, Unique = true)]
		[JsonProperty("material_id")]
		internal int MaterialId
		{
			get
			{
				return Material != null ? Material.ID : 0;
			}
			set
			{
				if (Material == null) Material = new Material();
				Material.ID = value;
			}
		}

		[Ignore]
		[JsonProperty("material_name")]
		internal string MaterialName
		{
			get
			{
				return Material != null ? Material.Name : string.Empty;
			}
			set
			{
				if (Material == null) Material = new Material();
				Material.Name = value;
			}
		}

		[Ignore]
		public Material Material { get; set; }

		[Column("share")]
		[JsonProperty("share")]
		public float Share { get; set; }
	}
}
