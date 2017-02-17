﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverworldObjects;

/// <summary>
/// governs what happens when you interact with a dwelling
/// </summary>
public class DwellingReact : Reaction {

    Dwelling dwelling;
    HeroMeetReact heroReact;

    public DwellingReact(Dwelling dwelling, Vector2 pos)
    {
        Dwelling = dwelling;
        Pos = pos;
        HeroReact = null;
    }

    public Dwelling Dwelling
    {
        get
        {
            return dwelling;
        }

        set
        {
            dwelling = value;
        }
    }

    public HeroMeetReact HeroReact
    {
        get
        {
            return heroReact;
        }

        set
        {
            heroReact = value;
        }
    }

    /// <summary>
    /// If you don't own the dwelling, change owner. Hire units that are at dwelling.
    /// </summary>
    /// <param name="h">The hero interacting with the dwelling</param>
    /// <returns>True if you do not own dwelling, else false.</returns>
    public override bool React(Hero h)
    {
        
        if (!dwelling.Owner.equals(h.Player))
        {
            if (dwelling.Owner != null) dwelling.Owner.DwellingsOwned.Remove(dwelling);
            dwelling.Owner = h.Player;
            h.Player.DwellingsOwned.Add(dwelling);
            //todo inital hiring of units
            return true;
        }
        
        // todo hire units
        return false;
    }
}
