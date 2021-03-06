﻿using System;
using OverworldObjects;
using Units;
using UI;

namespace TownView
{
    /// <summary>
    /// Troll Cave Unit building belonging to Viking Town
    /// </summary>
    public class TrollCave : UnitBuilding
    {
        const string name = "Troll Cave";
        const string description = "Stone trolls are famed for their cunning business sense. It's not rare to find their market caves floating in the sky.";
        static bool[] requirements = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false };
        const int LOCAL_SPRITEID = 5;
        const int LOCAL_SPRITEID_BLUEPRINT = 17;

        /// <summary>
        /// Resource cost for this building
        /// </summary>
        const int GOLD_COST = 1000;
        const int WOOD_COST = 5;
        const int ORE_COST = 0;
        const int CRYSTAL_COST = 0;
        const int GEM_COST = 0;


        /// <summary>
        /// Default constructor
        /// </summary>
        public TrollCave() : base(name, description, requirements, new Cost(GOLD_COST, WOOD_COST, ORE_COST, CRYSTAL_COST, GEM_COST), LOCAL_SPRITEID, LOCAL_SPRITEID_BLUEPRINT)
        {
            // Default starting values when building is built
            Unit = new StoneTroll();
            UnitsPerWeek = 5;
            UnitsPresent = 10; 
        }


        /// <summary>
        /// Override class to tell which card window this building uses
        /// </summary>
        /// <returns>Integer for which window type to display in the game</returns>
        protected override int GetUIType()
        {
            return UI.WindowTypes.DWELLING_CARD;
        }

    }
}
