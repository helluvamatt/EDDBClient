using Newtonsoft.Json;
using SQLite;

namespace EDDB.Data.Model
{
	[Table("body_atmosphere_components")]
	[JsonObject(MemberSerialization.OptIn)]
	public class BodyAtmosphereComponent
	{
		[Column("_id")]
		[PrimaryKey]
		[AutoIncrement]
		public int ID { get; set; }

		[Column("body_id")]
		[Indexed("uidx_body_atmosphere_component", 0, Unique = true)]
		internal int BodyId { get; set; }

		[Column("atmosphere_component_id")]
		[Indexed("uidx_body_atmosphere_component", 1, Unique = true)]
		[JsonProperty("atmosphere_component_id")]
		internal int AtmosphereComponentId
		{
			get
			{
				return AtmosphereComponent != null ? AtmosphereComponent.ID : -1;
			}
			set
			{
				if (AtmosphereComponent == null) AtmosphereComponent = new AtmosphereComponent();
				AtmosphereComponent.ID = value;
			}
		}

		[Ignore]
		[JsonProperty("atmosphere_component_name")]
		internal string AtmosphereComponentName
		{
			get
			{
				return AtmosphereComponent != null ? AtmosphereComponent.Name : null;
			}
			set
			{
				if (AtmosphereComponent == null) AtmosphereComponent = new AtmosphereComponent();
				AtmosphereComponent.Name = value;
			}
		}

		[Ignore]
		public AtmosphereComponent AtmosphereComponent { get; set; }

		[Column("share")]
		[JsonProperty("share")]
		public float Share { get; set; }
	}
}
