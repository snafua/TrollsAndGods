﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer
{
    public class Move : CombatEvent
    {
        public Move(int id, string description, int idFrom, int idTo) : base(id, description, idFrom, idTo)
        {
        }

        public override void execute()
        {
            throw new System.NotImplementedException();
        }
    } 
}
