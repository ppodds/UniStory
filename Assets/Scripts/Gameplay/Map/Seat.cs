using System;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    [Serializable]
    public class Seat
    {
        [SerializeField] private Vector2 position;
        
        private const float DeltaX = 10 / Constant.PixelsPerUnit;
        private const float DeltaY = 5 / Constant.PixelsPerUnit;
        public Seat(Wz_Node src)
        {
            var point = src.GetValue<Wz_Vector>();
            position = new Vector2(point.X, -point.Y) / Constant.PixelsPerUnit;
        }
        
        public bool IsInRange(Vector2 pos)
        {
            var range = new Rect(position.x - DeltaX, position.y, 2 * DeltaX, 2 * DeltaY);
            return range.Contains(pos);
        }
    }
}