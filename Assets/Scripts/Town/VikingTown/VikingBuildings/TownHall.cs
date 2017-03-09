﻿namespace TownView
{
    /// <summary>
    /// Placeholder class for a spesific building.
    /// Belongs to the "Unknown Town".
    /// </summary>
    public class TownHall : Building
    {
        // Required values for building:
        const string name = "Town Hall";
        const bool[] requirements = null;
        const int LOCAL_SPRITEID = 1;
        const int LOCAL_SPRITEID_BLUEPRINT = 6;

        // Window ID for UI
        const int WINDOW_TYPE = 0;

        // Resources cost: 
        const int GOLD_COST = 1000;
        const int WOOD_COST = 5;
        const int ORE_COST = 5;
        const int CRYSTAL_COST = 5;
        const int GEM_COST = 5;



        // This needs no indata since it knows its values.
        public TownHall() : base(name, requirements, new Cost(GOLD_COST, WOOD_COST, ORE_COST, CRYSTAL_COST, GEM_COST), LOCAL_SPRITEID, LOCAL_SPRITEID_BLUEPRINT)
        {
        }

        /// <summary>
        /// Override class to tell which card window this building uses
        /// </summary>
        /// <returns>Integer for which window type to display in the game</returns>
        protected override int GetUIType()
        {
            return UI.WindowTypes.TOWN_HALL_CARD;
        }
    }
}
