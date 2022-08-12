using System;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    [Serializable]
    public class Ladder
    {
        [SerializeField] private float x;

        // ladder top
        [SerializeField] private float top;

        // ladder bottom
        [SerializeField] private float bottom;
        [SerializeField] private bool isLadder;

        public float X => x;
        public float Top => top;
        public float Bottom => bottom;
        public bool IsLadder => isLadder;

        private const float DeltaX = 10 / Constant.PixelsPerUnit;
        private const float DeltaY = 5 / Constant.PixelsPerUnit;

        public Ladder(Wz_Node src)
        {
            x = src.FindNodeByPath("x").GetValue<int>() / Constant.PixelsPerUnit;
            top = -src.FindNodeByPath("y1").GetValue<int>() / Constant.PixelsPerUnit;
            bottom = -src.FindNodeByPath("y2").GetValue<int>() / Constant.PixelsPerUnit;
            isLadder = src.FindNodeByPath("l").GetValue<bool>();
        }

        public bool IsInRange(Vector2 position)
        {
            var range = new Rect(x - DeltaX, bottom, 2 * DeltaX, top - bottom + 2 * DeltaY);
            return range.Contains(position);
        }

        public bool FellOff(float y, bool downwards)
        {
            var dy = downwards ? y - DeltaY : y + DeltaY;
            return dy < bottom || y - DeltaY > top;
        }
    }
}