using EDDB.Data.Converters;
using Newtonsoft.Json;
using SQLite;
using System;

namespace EDDB.Data.Model
{
	[Table("rings")]
	[JsonObject(MemberSerialization.OptIn)]
	public class Ring
	{
		[Column("_id")]
		[JsonProperty("id")]
		public int ID { get; set; }

		[Column("body_id")]
		internal int BodyId { get; set; }

		[Column("name")]
		[JsonProperty("name")]
		public string Name { get; set; }

		[Column("semi_major_axis")]
		[JsonProperty("semi_major_axis")]
		public float SemiMajorAxis { get; set; }

		[Column("ring_mass")]
		[JsonProperty("ring_mass")]
		public int RingMass { get; set; }

		[Column("ring_inner_radius")]
		[JsonProperty("ring_inner_radius")]
		public int RingInnerRadius { get; set; }

		[Column("ring_outer_radius")]
		[JsonProperty("ring_outer_radius")]
		public int RingOuterRadius { get; set; }

		[Column("ring_type_id")]
		[JsonProperty("ring_type_id")]
		internal int RingTypeId
		{
			get
			{
				return RingType != null ? RingType.ID : -1;
			}
			set
			{
				if (RingType == null) RingType = new RingType();
				RingType.ID = value;
			}
		}

		[Ignore]
		[JsonProperty("ring_type_name")]
		internal string RingTypeName
		{
			get
			{
				return RingType != null ? RingType.Name : null;
			}
			set
			{
				if (RingType == null) RingType = new RingType();
				RingType.Name = value;
			}
		}

		[Ignore]
		public RingType RingType { get; set; }

		[Column("created_at")]
		[JsonProperty("created_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[JsonProperty("updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime UpdatedAt { get; set; }
	}
}
