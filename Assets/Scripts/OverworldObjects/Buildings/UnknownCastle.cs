﻿using OverworldObjects;
using TownView;

namespace OverworldObjects
{

    /// <summary>
    /// PLACEHOLDER CLASS FOR AN INITIALIZEABLE CASTLE.
    /// This class can be used as a template for a given castle.
    /// </summary>
    class UnknownCastle : Castle
    {
        // Shape of the sprite:
        const int shape = Shapes.CUBE;
        const string NAME = "unknown";

        // Unique sprite ID for this castle to be displayed in the overworld map: 
        private const int LOCAL_SPRITE_ID = 0;
        const IngameObjectLibrary.Category SPRITE_CATEGORY = IngameObjectLibrary.Category.Castle;

        // The enviroment that surrounds the castle:
        static Environment environmentTileType = new Grass();

        /// <summary>
        /// Default constructor:
        /// Sets all the needed values to be a castle. Also creates the town that belongs to
        /// the castle.
        /// </summary>
        /// <param name="origo">pkt the castle should be placed at.</param>
        /// <param name="owner">Player that owns the castle.</param>
        public UnknownCastle(Point origo, Player owner ) 
            : base(origo, shape, owner, LOCAL_SPRITE_ID, NAME, SPRITE_CATEGORY, environmentTileType)
        {
            Town = new VikingTown(owner, origo);
        }

        /*
        /// <summary>
        /// Sets all the needed values to be a castle. Also creates the town that belongs to
        /// the castle. This constructor allows the origopkt to be placed after the creation of the town.
        /// </summary>
        /// <param name="owner">Player that owns the castle.</param>
        public UnknownCastle( Player owner ) 
            : base( shape, owner, LOCAL_SPRITE_ID, SPRITE_CATEGORY)
        {
            Town = new VikingTown(owner);
        }
        */
    }
}
