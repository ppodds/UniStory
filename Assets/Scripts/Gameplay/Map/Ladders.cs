using System;
using System.Collections.Generic;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Map
{
    [Serializable]
    public class Ladders
    {
        [SerializeField] private List<Ladder> ladders = new List<Ladder>();

        public Ladders(WzObject src)
        {
            foreach (var ladder in ((WzSubProperty)src).WzProperties)
            {
                ladders.Add(new Ladder(ladder));
            }
        }
    }
}