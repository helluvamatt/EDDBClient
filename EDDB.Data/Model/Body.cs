using EDDB.Data.Converters;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;

namespace EDDB.Data.Model
{
	[Table("bodies")]
	[JsonObject(MemberSerialization.OptIn)]
	public class Body
	{
		[Column("_id")]
		[PrimaryKey]
		[JsonProperty("id")]
		public int ID { get; set; }

		[Column("name")]
		[JsonProperty("name")]
		public string Name { get; set; }

		[Column("system_id")]
		[JsonProperty("system_id")]
		internal int SystemId { get; set; }

		[Ignore]
		public StarSystem System { get; set; }

		[Column("group_id")]
		[JsonProperty("group_id")]
		internal int GroupId
		{
			get
			{
				return Group != null ? Group.ID : -1;
			}
			set
			{
				if (Group == null) Group = new BodyGroup();
				Group.ID = value;
			}
		}

		[Ignore]
		[JsonProperty("group_name")]
		internal string GroupName
		{
			get
			{
				return Group != null ? Group.Name : null;
			}
			set
			{
				if (Group == null) Group = new BodyGroup();
				Group.Name = value;
			}
		}

		[Ignore]
		public BodyGroup Group { get; set; }

		[Column("type_id")]
		[JsonProperty("type_id")]
		internal int? TypeId
		{
			get
			{
				return Type != null ? Type.ID : (int?)null;
			}
			set
			{
				if (value.HasValue)
				{
					if (Type == null) Type = new BodyType();
					Type.ID = value.Value;
				}
				else
				{
					Type = null;
				}
			}
		}

		[Ignore]
		[JsonProperty("type_name")]
		internal string TypeName
		{
			get
			{
				return Type != null ? Type.Name : null;
			}
			set
			{
				if (value != null)
				{
					if (Type == null) Type = new BodyType();
					Type.Name = value;
				}
				else
				{
					Type = null;
				}
			}
		}

		[Ignore]
		public BodyType Type { get; set; }

		[Column("distance_to_arrival")]
		[JsonProperty("distance_to_arrival")]
		public int? DistanceToArrival { get; set; }

		[Column("is_landable")]
		[JsonProperty("is_landable")]
		public bool IsLandable { get; set; }

		[Column("full_spectral_class")]
		[JsonProperty("full_spectral_class")]
		public string FullSpectralClass { get; set; }

		[Column("spectral_class")]
		[JsonProperty("spectral_class")]
		public string SpectralClass { get; set; }

		[Column("spectral_sub_class")]
		[JsonProperty("spectral_sub_class")]
		public string SpectralSubClass { get; set; }

		[Column("luminosity_class")]
		[JsonProperty("luminosity_class")]
		public string LuminosityClass { get; set; }

		[Column("luminosity_sub_class")]
		[JsonProperty("luminosity_sub_class")]
		public string LuminositySubClass { get; set; }

		[Column("surface_temperature")]
		[JsonProperty("surface_temperature")]
		public float? SurfaceTemperature { get; set; }

		[Column("is_main_star")]
		[JsonProperty("is_main_star")]
		public bool? IsMainStar { get; set; }

		[Column("age")]
		[JsonProperty("age")]
		public float? Age { get; set; }

		[Column("solar_masses")]
		[JsonProperty("solar_masses")]
		public float? SolarMasses { get; set; }

		[Column("solar_radius")]
		[JsonProperty("solar_radius")]
		public float? SolarRadius { get; set; }

		[Column("catalogue_gliese_id")]
		[JsonProperty("catalogue_gliese_id")]
		public string CatalogueGlieseId { get; set; }

		[Column("catalogue_hipp_id")]
		[JsonProperty("catalogue_hipp_id")]
		public string CatalogueHippId { get; set; }
		
		[Column("catalogue_hd_id")]
		[JsonProperty("catalogue_hd_id")]
		public string CatalogueHdId { get; set; }
		
		[Column("volcanism_type_id")]
		[JsonProperty("volcanism_type_id")]
		internal int? VolcanismTypeId
		{
			get
			{
				return VolcanismType != null ? VolcanismType.ID : (int?)null;
			}
			set
			{
				if (value.HasValue)
				{
					if (VolcanismType == null) VolcanismType = new VolcanismType();
					VolcanismType.ID = value.Value;
				}
				else
				{
					VolcanismType = null;
				}
			}
		}

		[Ignore]
		[JsonProperty("volcanism_type_name")]
		internal string VolcanismTypeName
		{
			get
			{
				return VolcanismType != null ? VolcanismType.Name : null;
			}
			set
			{
				if (value != null)
				{
					if (VolcanismType == null) VolcanismType = new VolcanismType();
					VolcanismType.Name = value;
				}
				else
				{
					VolcanismType = null;
				}
			}
		}

		[Ignore]
		public VolcanismType VolcanismType { get; set; }

		[Column("atmosphere_type_id")]
		[JsonProperty("atmosphere_type_id")]
		internal int? AtmosphereTypeId
		{
			get
			{
				return AtmosphereType != null ? AtmosphereType.ID : (int?)null;
			}
			set
			{
				if (value.HasValue)
				{
					if (AtmosphereType == null) AtmosphereType = new AtmosphereType();
					AtmosphereType.ID = value.Value;
				}
				else
				{
					AtmosphereType = null;
				}
			}
		}

		[Ignore]
		[JsonProperty("atmosphere_type_name")]
		internal string AtmosphereTypeName
		{
			get
			{
				return AtmosphereType != null ? AtmosphereType.Name : null;
			}
			set
			{
				if (value != null)
				{
					if (AtmosphereType == null) AtmosphereType = new AtmosphereType();
					AtmosphereType.Name = value;
				}
				else
				{
					AtmosphereType = null;
				}
			}
		}

		[Ignore]
		public AtmosphereType AtmosphereType { get; set; }

		[Column("terraforming_state_id")]
		[JsonProperty("terraforming_state_id")]
		internal int? TerraformingStateId
		{
			get
			{
				return TerraformingState != null ? TerraformingState.ID : (int?)null;
			}
			set
			{
				if (value.HasValue)
				{
					if (TerraformingState == null) TerraformingState = new TerraformingState();
					TerraformingState.ID = value.Value;
				}
				else
				{
					TerraformingState = null;
				}
			}
		}

		[Ignore]
		[JsonProperty("terraforming_state_name")]
		internal string TerraformingStateName
		{
			get
			{
				return TerraformingState != null ? TerraformingState.Name : null;
			}
			set
			{
				if (value != null)
				{
					if (TerraformingState == null) TerraformingState = new TerraformingState();
					TerraformingState.Name = value;
				}
				else
				{
					TerraformingState = null;
				}
			}
		}

		[Ignore]
		public TerraformingState TerraformingState { get; set; }

		[Column("earth_massess")]
		[JsonProperty("earth_massess")]
		public float? EarthMasses { get; set; }

		[Column("radius")]
		[JsonProperty("radius")]
		public float? Radius { get; set; }

		[Column("gravity")]
		[JsonProperty("gravity")]
		public float? Gravity { get; set; }

		[Column("surface_pressure")]
		[JsonProperty("surface_pressure")]
		public float? SurfacePressure { get; set; }

		[Column("orbital_period")]
		[JsonProperty("orbital_period")]
		public float? OrbitalPeriod { get; set; }

		[Column("semi_major_axis")]
		[JsonProperty("semi_major_axis")]
		public float? SemiMajorAxis { get; set; }

		[Column("orbital_eccentricity")]
		[JsonProperty("orbital_eccentricity")]
		public float? OrbitalEccentricity { get; set; }

		[Column("orbital_inclination")]
		[JsonProperty("orbital_inclination")]
		public float? OrbitalInclination { get; set; }

		[Column("arg_of_periapsis")]
		[JsonProperty("arg_of_periapsis")]
		public float? ArgOfPeriapsis { get; set; }

		[Column("rotational_period")]
		[JsonProperty("rotational_period")]
		public float? RotationalPeriod { get; set; }

		[Column("is_rotation_period_tidally_locked")]
		[JsonProperty("is_rotation_period_tidally_locked")]
		public bool IsRotationPeriodTidallyLocked { get; set; }

		[Column("axis_tilt")]
		[JsonProperty("axis_tilt")]
		public float? AxisTilt { get; set; }

		[Column("eg_id")]
		[JsonProperty("eg_id")]
		public int? EgId { get; set; }

		[Column("belt_moon_masses")]
		[JsonProperty("belt_moon_masses")]
		public float? BeltMoonMasses { get; set; }

		[JsonProperty("rings")]
		[Ignore]
		public ISet<Ring> Rings { get; set; }

		[JsonProperty("atmosphere_composition")]
		[Ignore]
		public ISet<BodyAtmosphereComponent> AtmosphereComposition { get; set; }

		[JsonProperty("solid_composition")]
		[Ignore]
		public ISet<BodySolidComponent> SolidComposition { get; set; }

		[JsonProperty("materials")]
		[Ignore]
		public ISet<BodyMaterial> Materials { get; set; }

		[Column("created_at")]
		[JsonProperty("created_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[JsonProperty("updated_at", ItemConverterType = typeof(UnixTimestampConverter))]
		public DateTime UpdatedAt { get; set; }
	}
}
