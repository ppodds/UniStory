using System;
using System.Collections.Generic;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    [Serializable]
    public class Ladders
    {
        [SerializeField] private List<Ladder> ladders = new List<Ladder>();

        public Ladders(Wz_Node src)
        {
            foreach (var ladder in src.Nodes)
            {
                ladders.Add(new Ladder(ladder));
            }
        }
    }
}