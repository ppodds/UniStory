using System;
using System.Collections.Generic;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using UnityEngine;

namespace Gameplay.Map
{
    [Serializable]
    public class Seats
    {
        [SerializeField] private List<Seat> seats = new List<Seat>();

        public Seats(WzObject src)
        {
            foreach (var seat in ((WzSubProperty)src).WzProperties)
            {
                seats.Add(new Seat(seat));
            }
        }
    }
}