using System;
using System.Collections.Generic;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    [Serializable]
    public class Seats
    {
        [SerializeField] private List<Seat> seats = new List<Seat>();

        public Seats(Wz_Node src)
        {
            if (src == null) return;
            foreach (var seat in src.Nodes)
            {
                seats.Add(new Seat(seat));
            }
        }
    }
}