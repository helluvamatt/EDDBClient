using EDDB.Data.Model;
using EDDB.Data.Model.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDDB.Data
{
    public class EDDBContext : DbContext 
    {
		public EDDBContext(DbContextOptions<EDDBContext> options) : base(options) { }

		#region Internal interface (used for importing)

		internal void SaveBody(Body body)
		{
			if (body == null) throw new ArgumentNullException("body");

			using (LockDatabase())
			{
				database.InsertOrReplace(body);
				if (body.Group != null)
				{
					database.InsertOrReplace(body.Group);
				}
				if (body.Type != null)
				{
					database.InsertOrReplace(body.Type);
				}
				if (body.VolcanismType != null)
				{
					database.InsertOrReplace(body.VolcanismType);
				}
				if (body.AtmosphereType != null)
				{
					database.InsertOrReplace(body.AtmosphereType);
				}
				if (body.TerraformingState != null)
				{
					database.InsertOrReplace(body.TerraformingState);
				}
				if (body.Rings != null)
				{
					foreach (Ring ring in body.Rings)
					{
						ring.BodyId = body.ID;
						database.InsertOrReplace(ring);
						if (ring.RingType != null)
						{
							database.InsertOrReplace(ring.RingType);
						}
					}
				}
				if (body.AtmosphereComposition != null)
				{
					foreach (BodyAtmosphereComponent bodyAtmosphereComponent in body.AtmosphereComposition)
					{
						bodyAtmosphereComponent.BodyId = body.ID;
						database.InsertOrReplace(bodyAtmosphereComponent);
						if (bodyAtmosphereComponent.AtmosphereComponent != null)
						{
							database.InsertOrReplace(bodyAtmosphereComponent.AtmosphereComponent);
						}
					}
				}
				if (body.SolidComposition != null)
				{
					foreach (BodySolidComponent bodySolidComponent in body.SolidComposition)
					{
						bodySolidComponent.BodyId = body.ID;
						database.InsertOrReplace(bodySolidComponent);
						if (bodySolidComponent.SolidComponent != null)
						{
							database.InsertOrReplace(bodySolidComponent);
						}
					}
				}
				if (body.Materials != null)
				{
					foreach (BodyMaterial bodyMaterial in body.Materials)
					{
						bodyMaterial.BodyId = body.ID;
						database.InsertOrReplace(bodyMaterial);
						if (bodyMaterial.Material != null)
						{
							database.InsertOrReplace(bodyMaterial.Material);
						}
					}
				}
			}
		}

		internal void SaveCommodity(Commodity commodity)
		{
			if (commodity == null) throw new ArgumentNullException("commodity");

			using (LockDatabase())
			{
				database.InsertOrReplace(commodity);
				if (commodity.Category != null)
				{
					database.InsertOrReplace(commodity.Category);
				}
			}
		}

		internal void SaveModule(Module module)
		{
			if (module == null) throw new ArgumentNullException("module");

			using (LockDatabase())
			{
				if (!string.IsNullOrWhiteSpace(module.ShipName))
				{
					var ship = GetShip(module.ShipName);
					if (ship == null)
					{
						ship = new Ship();
						ship.Name = module.ShipName;
						ship.ID = database.Insert(ship);
					}
					module.ShipId = ship.ID;
				}
				database.InsertOrReplace(module);
				if (module.Group != null)
				{
					database.InsertOrReplace(module.Group);
					if (module.Group.Category != null)
					{
						database.InsertOrReplace(module.Group.Category);
					}
				}
			}
		}

		internal void SaveListing(Listing listing)
		{
			if (listing == null) throw new ArgumentNullException("listing");

			using (LockDatabase())
			{
				database.InsertOrReplace(listing);
				if (listing.Commodity != null)
				{
					database.InsertOrReplace(listing.Commodity);
				}
				if (listing.Station != null)
				{
					database.InsertOrReplace(listing.Station);
				}
			}
		}

		internal void SaveMinorFaction(MinorFaction minorFaction)
		{
			if (minorFaction == null) throw new ArgumentNullException("minorFaction");

			using (LockDatabase())
			{
				database.InsertOrReplace(minorFaction);
				if (minorFaction.Allegiance != null)
				{
					database.InsertOrReplace(minorFaction.Allegiance);
				}
				if (minorFaction.Government != null)
				{
					database.InsertOrReplace(minorFaction.Government);
				}
				if (minorFaction.HomeSystem != null)
				{
					database.InsertOrReplace(minorFaction.HomeSystem);
				}
				if (minorFaction.State != null)
				{
					database.InsertOrReplace(minorFaction.State);
				}
			}
		}

		internal void SaveStation(Station station)
		{
			if (station == null) throw new ArgumentNullException("station");

			using (LockDatabase())
			{
				database.InsertOrReplace(station);

				database.Execute("DELETE FROM station_import_commodities WHERE station_id = ?", station.ID);
				if (station.ImportCommodityNames != null && station.ImportCommodityNames.Count > 0)
				{
					foreach (var importComName in station.ImportCommodityNames)
					{
						Commodity commodity = GetCommodity(importComName);
						if (commodity != null)
						{
							StationImportCommodity sic = new StationImportCommodity();
							sic.StationId = station.ID;
							sic.CommodityId = commodity.ID;
							database.InsertOrReplace(sic);
						}
					}
				}

				database.Execute("DELETE FROM station_export_commodities WHERE station_id = ?", station.ID);
				if (station.ExportCommodityNames != null && station.ExportCommodityNames.Count > 0)
				{
					foreach (var exportComName in station.ExportCommodityNames)
					{
						Commodity commodity = GetCommodity(exportComName);
						if (commodity != null)
						{
							StationExportCommodity sec = new StationExportCommodity();
							sec.StationId = station.ID;
							sec.CommodityId = commodity.ID;
							database.InsertOrReplace(sec);
						}
					}
				}

				database.Execute("DELETE FROM station_banned_commodities WHERE station_id = ?", station.ID);
				if (station.BannedCommodityNames != null && station.BannedCommodityNames.Count > 0)
				{
					foreach (var bannedComName in station.BannedCommodityNames)
					{
						Commodity commodity = GetCommodity(bannedComName);
						if (commodity != null)
						{
							StationBannedCommodity sbc = new StationBannedCommodity();
							sbc.StationId = station.ID;
							sbc.CommodityId = commodity.ID;
							database.InsertOrReplace(sbc);
						}
					}
				}

				database.Execute("DELETE FROM station_economy WHERE station_id = ?", station.ID);
				if (station.EconomyNames != null && station.EconomyNames.Count > 0)
				{
					foreach (var stationEconName in station.EconomyNames)
					{
						Economy economy = GetEconomy(stationEconName);
						if (economy != null)
						{
							StationEconomy se = new StationEconomy();
							se.StationId = station.ID;
							se.EconomyId = economy.ID;
							database.InsertOrReplace(se);
						}
					}
				}

				database.Execute("DELETE FROM station_selling_module WHERE station_id = ?", station.ID);
				if (station.SellingModuleIds != null && station.SellingModuleIds.Count > 0)
				{
					foreach (int moduleId in station.SellingModuleIds)
					{
						StationSellingModule ssm = new StationSellingModule();
						ssm.StationId = station.ID;
						ssm.ModuleId = moduleId;
						database.InsertOrReplace(ssm);
					}
				}

				database.Execute("DELETE FROM station_selling_ship WHERE station_id = ?", station.ID);
				if (station.SellingShipNames != null && station.SellingShipNames.Count > 0)
				{
					foreach (var shipName in station.SellingShipNames)
					{
						Ship ship = GetShip(shipName);
						if (ship != null)
						{
							StationSellingShip sss = new StationSellingShip();
							sss.StationId = station.ID;
							sss.ShipId = ship.ID;
							database.InsertOrReplace(sss);
						}
					}
				}

			}
		}

		internal void SaveStarSystem(StarSystem system)
		{
			if (system == null) throw new ArgumentNullException("system");

			using (LockDatabase())
			{
				database.InsertOrReplace(system);
				if (system.Government != null)
				{
					database.InsertOrReplace(system.Government);
				}
				if (system.Allegiance != null)
				{
					database.InsertOrReplace(system.Allegiance);
				}
				if (system.State != null)
				{
					database.InsertOrReplace(system.State);
				}
				if (system.Security != null)
				{
					database.InsertOrReplace(system.Security);
				}
				if (system.PrimaryEconomy != null)
				{
					database.InsertOrReplace(system.PrimaryEconomy);
				}
				if (system.ReserveType != null)
				{
					database.InsertOrReplace(system.ReserveType);
				}
			}
		}

		internal void ClearStarSystemMinorFactionPresences(int starSystemId)
		{
			using (LockDatabase())
			{
				database.Execute("DELETE FROM star_system_minor_faction_presence WHERE system_id = ?", starSystemId);
			}
		}

		internal void SaveStarSystemMinorFactionPresence(StarSystemMinorFactionPresence starSystemMinorFactionPresence)
		{
			if (starSystemMinorFactionPresence == null) throw new ArgumentNullException("starSystemMinorFactionPresence");

			using (LockDatabase())
			{
				database.InsertOrReplace(starSystemMinorFactionPresence);
			}
		}

		#endregion

		#region Public interface

		public ILock LockDatabase()
		{
			return ConcurrencyLock.Obtain(lockObject);
		}

		#region Commodities

		public Commodity GetCommodity(int id)
		{
			using (LockDatabase())
			{
				var c = database.Get<Commodity>(id);
				if (c != null)
				{
					c.Category = database.Get<CommodityCategory>(c.CategoryId);
				}
				return c;
			}
		}

		public Commodity GetCommodity(string name)
		{
			using (LockDatabase())
			{
				var commodity = database.Table<Commodity>().SingleOrDefault(c => c.Name == name);
				if (commodity != null)
				{
					commodity.Category = database.Get<CommodityCategory>(commodity.CategoryId);
				}
				return commodity;
			}
		}

		public ISet<Commodity> GetCommodities()
		{
			using (LockDatabase())
			{
				return new HashSet<Commodity>(database.Table<Commodity>().Join(database.Table<CommodityCategory>(), c => c.CategoryId, cat => cat.ID, (c, cat) => c.BuildWithCategory(cat)));
			}
		}

		#endregion

		#region Modules

		public Module GetModule(int id)
		{
			using (LockDatabase())
			{
				var m = database.Get<Module>(id);
				if (m != null)
				{
					m.Group = database.Get<ModuleGroup>(m.GroupID);
					if (m.Group != null)
					{
						m.Group.Category = database.Get<ModuleGroupCategory>(m.GroupID);
					}
				}
				return m;
			}
		}

		public ISet<ModuleGroup> GetModuleGroups()
		{
			using (LockDatabase())
			{
				return new HashSet<ModuleGroup>(database.Table<ModuleGroup>()
					.Join(database.Table<ModuleGroupCategory>(), mg => mg.CategoryId, cat => cat.ID, (mg, cat) => mg.BuildWithCategory(cat))
					.Join(database.Table<Module>(), mg => mg.ID, m => m.GroupID, (mg, m) => mg.BuildWithModuleChild(m)));
			}
		}

		#endregion

		#region Star systems

		public StarSystem GetStarSystem(int id)
		{
			using (LockDatabase())
			{
				var starSystem = database.Get<StarSystem>(id);
				if (starSystem != null)
				{
					starSystem.Government = GetGovernment(starSystem.GovernmentId);
					starSystem.Allegiance = GetSuperpower(starSystem.AllegianceId);
					starSystem.State = GetEconomicState(starSystem.StateId);
					starSystem.Security = GetSecurity(starSystem.SecurityId);
					starSystem.PrimaryEconomy = GetEconomy(starSystem.PrimaryEconomyId);
					starSystem.ControllingMinorFaction = GetMinorFaction(starSystem.ControllingMinorFactionId);
					starSystem.ReserveType = GetReserveType(starSystem.ReserveTypeId);
					starSystem.MinorFactionPresences = new HashSet<StarSystemMinorFactionPresence>(database.Table<StarSystemMinorFactionPresence>().Where(ssmfp => ssmfp.StarSystemId == starSystem.ID));
				}
				return starSystem;
			}
		}

		public ISet<StarSystem> GetStarSystems()
		{
			using (LockDatabase())
			{
				return new HashSet<StarSystem>(database.Table<StarSystem>());
			}
		}

		#endregion

		#region Minor factions

		public MinorFaction GetMinorFaction(int id)
		{
			using (LockDatabase())
			{
				MinorFaction minorFaction = database.Get<MinorFaction>(id);
				if (minorFaction.GovernmentId.HasValue)
				{
					minorFaction.Government = GetGovernment(minorFaction.GovernmentId.Value);
				}
				if (minorFaction.AllegianceId.HasValue)
				{
					minorFaction.Allegiance = GetSuperpower(minorFaction.AllegianceId.Value);
				}
				if (minorFaction.StateId.HasValue)
				{
					minorFaction.State = GetEconomicState(minorFaction.StateId.Value);
				}
				minorFaction.HomeSystem = GetStarSystem(minorFaction.HomeSystemId);
				return minorFaction;
			}
		}

		public ISet<MinorFaction> GetMinorFactions()
		{
			using (LockDatabase())
			{
				return new HashSet<MinorFaction>(database.Table<MinorFaction>());
			}
		}

		#endregion

		#region Bodies

		// TODO Need to implement bodies fully
		/*
		public Body GetBody(int id)
		{
			using (LockDatabase())
			{
				// TODO Join relationship fields
				return database.Get<Body>(id);
			}
		}

		public ISet<Body> GetBodies()
		{
			using (LockDatabase())
			{
				return new HashSet<Body>(database.Table<Body>());
			}
		}
		*/

		#endregion

		#region Stations

		public Station GetStation(int id)
		{
			using (LockDatabase())
			{
				var station = database.Get<Station>(id);
				if (station != null)
				{
					station.System = GetStarSystem(station.SystemID);
					station.Government = GetGovernment(station.GovernmentId);
					station.Allegiance = GetSuperpower(station.AllegianceId);
					station.State = GetEconomicState(station.StateId);
					station.Type = GetStationType(station.TypeId);
					station.ControllingMinorFaction = GetMinorFaction(station.ControllingMinorFactionId);

					if (station.SettlementSizeId.HasValue)
					{
						station.SettlementSize = GetSettlementSize(station.SettlementSizeId.Value);
					}

					if (station.SettlementSecurityId.HasValue)
					{
						station.SettlementSecurity = GetSecurity(station.SettlementSecurityId.Value);
					}

					// TODO Need to implement bodies full
					/*
					if (station.BodyId.HasValue)
					{
						station.Body = GetBody(station.BodyId.Value);
					}
					*/

					foreach (var stationImportCom in database.Table<StationImportCommodity>().Where(sic => sic.StationId == station.ID))
					{
						Commodity commodity = GetCommodity(stationImportCom.CommodityId);
						if (commodity != null)
						{
							if (station.ImportCommodities == null) station.ImportCommodities = new HashSet<Commodity>();
							station.ImportCommodities.Add(commodity);
						}
					}

					foreach (var stationExportCom in database.Table<StationExportCommodity>().Where(sec => sec.StationId == station.ID))
					{
						Commodity commodity = GetCommodity(stationExportCom.CommodityId);
						if (commodity != null)
						{
							if (station.ExportCommodities == null) station.ExportCommodities = new HashSet<Commodity>();
							station.ExportCommodities.Add(commodity);
						}
					}

					foreach (var stationBannedCom in database.Table<StationBannedCommodity>().Where(sbc => sbc.StationId == station.ID))
					{
						Commodity commodity = GetCommodity(stationBannedCom.CommodityId);
						if (commodity != null)
						{
							if (station.BannedCommodities == null) station.BannedCommodities = new HashSet<Commodity>();
							station.BannedCommodities.Add(commodity);
						}
					}

					foreach (var stationEconomy in database.Table<StationEconomy>().Where(se => se.StationId == station.ID))
					{
						Economy economy = GetEconomy(stationEconomy.EconomyId);
						if (economy != null)
						{
							if (station.Economies == null) station.Economies = new HashSet<Economy>();
							station.Economies.Add(economy);
						}
					}

					foreach (var sellingModules in database.Table<StationSellingModule>().Where(ssm => ssm.StationId == station.ID))
					{
						Module module = GetModule(sellingModules.ModuleId);
						if (module != null)
						{
							if (station.SellingModules == null) station.SellingModules = new HashSet<Module>();
							station.SellingModules.Add(module);
						}
					}

					foreach (var sellingShips in database.Table<StationSellingShip>().Where(sss => sss.StationId == station.ID))
					{
						Ship ship = GetShip(sellingShips.ShipId);
						if (ship != null)
						{
							if (station.SellingShips == null) station.SellingShips = new HashSet<Ship>();
							station.SellingShips.Add(ship);
						}
					}

				}
				return station;
			}
		}

		public ISet<Station> GetStations()
		{
			using (LockDatabase())
			{
				return new HashSet<Station>(database.Table<Station>());
			}
		}

		#endregion

		#region Economies

		public ISet<Economy> GetEconomies()
		{
			return GetBaseObjects<Economy>();
		}

		public Economy GetEconomy(string name)
		{
			return GetBaseObject<Economy>(name);
		}

		public Economy GetEconomy(int id)
		{
			return GetBaseObject<Economy>(id);
		}

		#endregion

		#region Ships

		public ISet<Ship> GetShips()
		{
			return GetBaseObjects<Ship>();
		}

		public Ship GetShip(string name)
		{
			return GetBaseObject<Ship>(name);
		}

		public Ship GetShip(int id)
		{
			return GetBaseObject<Ship>(id);
		}

		public ISet<Station> GetStationsSellingShip(Ship ship)
		{
			if (ship == null) throw new ArgumentNullException("ship");

			using (LockDatabase())
			{
				return new HashSet<Station>(database.Table<StationSellingShip>().Where(sss => sss.ShipId == ship.ID).Select(sss2 => GetStation(sss2.StationId)));
			}
		}

		#endregion

		#region Governments

		public ISet<Government> GetGovernments()
		{
			return GetBaseObjects<Government>();
		}

		public Government GetGovernment(int id)
		{
			return GetBaseObject<Government>(id);
		}

		#endregion

		#region Superpowers

		public ISet<Superpower> GetSuperpowers()
		{
			return GetBaseObjects<Superpower>();
		}

		public Superpower GetSuperpower(int id)
		{
			return GetBaseObject<Superpower>(id);
		}

		#endregion

		#region Economic states

		public ISet<EconomicState> GetEconomicStates()
		{
			return GetBaseObjects<EconomicState>();
		}

		public EconomicState GetEconomicState(int id)
		{
			return GetBaseObject<EconomicState>(id);
		}

		#endregion

		#region Station types

		public ISet<StationType> GetStationTypes()
		{
			return GetBaseObjects<StationType>();
		}

		public StationType GetStationType(int id)
		{
			return GetBaseObject<StationType>(id);
		}

		#endregion

		#region Settlement sizes

		public ISet<SettlementSize> GetSettlementSizes()
		{
			return GetBaseObjects<SettlementSize>();
		}

		public SettlementSize GetSettlementSize(int id)
		{
			return GetBaseObject<SettlementSize>(id);
		}

		#endregion

		#region Security

		public ISet<Security> GetSecurities()
		{
			return GetBaseObjects<Security>();
		}

		public Security GetSecurity(int id)
		{
			return GetBaseObject<Security>(id);
		}

		#endregion

		#region Reserve type

		public ISet<ReserveType> GetReserveTypes()
		{
			return GetBaseObjects<ReserveType>();
		}

		public ReserveType GetReserveType(int id)
		{
			return GetBaseObject<ReserveType>(id);
		} 

		#endregion

		#endregion

		#region Helpers

		private T GetBaseObject<T>(string name) where T : BaseObject, new()
		{
			using (LockDatabase())
			{
				return database.Table<T>().SingleOrDefault(bo => bo.Name == name);
			}
		}

		private T GetBaseObject<T>(int id) where T : BaseObject, new()
		{
			using (LockDatabase())
			{
				return database.Get<T>(id);
			}
		}

		private ISet<T> GetBaseObjects<T>() where T : BaseObject, new()
		{
			using (LockDatabase())
			{
				return new HashSet<T>(database.Table<T>());
			}
		}

		#endregion
	}
}
