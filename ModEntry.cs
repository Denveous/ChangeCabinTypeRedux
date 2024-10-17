using Microsoft.Xna.Framework;
using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Locations;
using System;
using System.Runtime.CompilerServices;
using xTile.Dimensions;
#nullable disable
namespace ChangeCabinType {
  internal class ModEntry : Mod {
    private ModConfig Config;
    private int keypressCounter = 0;
    string[] cabinTypes = { "Plank Cabin", "Log Cabin", "Neighbor Cabin", "Rustic Cabin", "Beach Cabin", "Trailer Cabin" };
    public override void Entry(IModHelper helper) {
      this.Config = this.Helper.ReadConfig<ModConfig>();
      helper.Events.Input.ButtonPressed += new EventHandler<ButtonPressedEventArgs>(this.ControlEvents_KeyPressed);
    }
    private void ChangeCabin(Building srcBuilding) {
      if (srcBuilding == null) { return; }
      ((NetFieldBase<string, NetString>) srcBuilding.skinId).Value = cabinTypes[this.keypressCounter];
    }
    private void ControlEvents_KeyPressed(object sender, ButtonPressedEventArgs e) {
      this.Helper.ReadConfig<ModConfig>();
      if (!Context.IsWorldReady || !(e.Button.ToString() == this.Config.cabinChangeHotKey)) return;
      Building getBuilding = ((Game1.getLocationFromName("Farm") as Farm)).getBuildingAt(new Vector2((float) ((((xTile.Dimensions.Rectangle) Game1.viewport).X + Game1.getOldMouseX()) / 64), (float) ((((xTile.Dimensions.Rectangle) Game1.viewport).Y + Game1.getOldMouseY()) / 64)));
      if (getBuilding != null && ((NetFieldBase<string, NetString>) getBuilding.buildingType).Value == "Cabin") {
        ChangeCabin(getBuilding);
        this.keypressCounter = (this.keypressCounter + 1) % cabinTypes.Length;
      }
    }
  }
}
