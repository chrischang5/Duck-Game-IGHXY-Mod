﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("MyMod")]

// The author of the mod
[assembly: AssemblyCompany("Me, The Creator")]

// The description of the mod
[assembly: AssemblyDescription("This Mod Does All The Things!")]

// The mod's version
[assembly: AssemblyVersion("1.0.0.0")]

namespace DuckGame.MyMod
{
    public class MyMod : Mod
    {
		// The mod's priority; this property controls the load order of the mod.
		public override Priority priority
		{
			get { return base.priority; }
		}

		// This function is run before all mods are finished loading.
		protected override void OnPreInitialize()
		{
			base.OnPreInitialize();
		}

		// This function is run after all mods are loaded.
		protected override void OnPostInitialize()
		{
			base.OnPostInitialize();
		}
	}
}
