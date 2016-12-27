using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;

namespace EDDB.Data.Model
{
	[Table("module_group")]
	public class ModuleGroup
	{
		public ModuleGroup()
		{
			Modules = new List<Module>();
		}

		[JsonProperty("id")]
		[PrimaryKey]
		[Column("_id")]
		public int ID { get; set; }

		[JsonProperty("name")]
		[Column("name")]
		public string Name { get; set; }

		[JsonIgnore]
		[Ignore]
		public ModuleGroupCategory Category { get; set; }

		[JsonProperty("category_id")]
		[Column("category_id")]
		internal int CategoryId { get; set; }

		[Ignore]
		[JsonProperty("category")]
		internal string CategoryName
		{
			set
			{
				if (Category == null) Category = new ModuleGroupCategory();
				Category.Name = value;
			}
		}

		[Ignore]
		[JsonIgnore]
		public List<Module> Modules { get; private set; }

		internal ModuleGroup BuildWithCategory(ModuleGroupCategory cat)
		{
			Category = cat;
			return this;
		}

		internal ModuleGroup BuildWithModuleChild(Module m)
		{
			Modules.Add(m);
			return this;
		}
	}
}
