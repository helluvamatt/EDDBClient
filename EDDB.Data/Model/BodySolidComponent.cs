using Newtonsoft.Json;
using SQLite;

namespace EDDB.Data.Model
{
	[Table("body_solid_components")]
	[JsonObject(MemberSerialization.OptIn)]
	public class BodySolidComponent
	{
		[Column("_id")]
		[PrimaryKey]
		[AutoIncrement]
		public int ID { get; set; }

		[Column("body_id")]
		[Indexed("uidx_body_solid_component", 0, Unique = true)]
		internal int BodyId { get; set; }

		[Column("solid_component_id")]
		[Indexed("uidx_body_solid_component", 1, Unique = true)]
		[JsonProperty("solid_component_id")]
		internal int SolidComponentId
		{
			get
			{
				return SolidComponent != null ? SolidComponent.ID : -1;
			}
			set
			{
				if (SolidComponent == null) SolidComponent = new SolidComponent();
				SolidComponent.ID = value;
			}
		}

		[Ignore]
		[JsonProperty("solid_component_name")]
		internal string SolidComponentName
		{
			get
			{
				return SolidComponent != null ? SolidComponent.Name : null;
			}
			set
			{
				if (SolidComponent == null) SolidComponent = new SolidComponent();
				SolidComponent.Name = value;
			}
		}

		[Ignore]
		public SolidComponent SolidComponent { get; set; }

		[Column("share")]
		[JsonProperty("share")]
		public float Share { get; set; }

	}
}
